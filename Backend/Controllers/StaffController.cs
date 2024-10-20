using Microsoft.AspNetCore.Mvc;
using DDDSample1.Domain.Staff;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using DDDSample1.Domain.Users;
using DDDSample1.Users;
using DDDSample1.Domain.Specialization;
using Backend.Domain.Staff.ValueObjects;

namespace DDDSample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly StaffService _staffService;
        private readonly UserService _userService;
        private readonly SpecializationService _specializationService;

        public StaffController(StaffService staffService, UserService userService, SpecializationService specializationService)
        {
            _staffService = staffService;
            _userService = userService;
            _specializationService = specializationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDTO>>> GetAllStaff()
        {
            var staffList = await _staffService.GetAllAsync();
            return Ok(staffList);
        }

        [HttpGet("{licenseNumber}")]
        public async Task<ActionResult<StaffDTO>> GetStaffByLicenseNumber(string licenseNumber)
        {
            var staff = await _staffService.GetByLicenseNumberAsync(new LicenseNumber(licenseNumber));

            if (staff == null)
            {
                return NotFound();
            }

            return Ok(staff);
        }

        [HttpPost]
        public async Task<ActionResult<StaffDTO>> CreateStaff(CreatingStaffDTO staffDto)
        {
            var user = await _userService.GetByIdAsync(new UserId(staffDto.UserId));

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            var specialization = await _specializationService.GetBySpecializationIdAsync(new SpecializationId(staffDto.SpecializationId));

            if (specialization == null)
            {
                return NotFound(new { Message = "Specialization not found" });
            }

            var availabilitySlots = staffDto.AvailabilitySlots != null
                ? new AvailabilitySlots(staffDto.AvailabilitySlots.Select(slot => new AvailabilitySlot(slot.Start, slot.End)).ToList())
                : new AvailabilitySlots();

            var staff = new Staff(
                new UserId(staffDto.UserId),
                new LicenseNumber(staffDto.LicenseNumber),
                new SpecializationId(staffDto.SpecializationId),
                availabilitySlots
            );

            var createdStaff = await _staffService.AddAsync(staffDto);

            return CreatedAtAction(nameof(GetStaffByLicenseNumber), new { licenseNumber = createdStaff.LicenseNumber }, createdStaff);
        }


        [HttpPut("{licenseNumber}")]
        public async Task<ActionResult<StaffDTO>> UpdateStaff(string licenseNumber, StaffDTO dto)
        {
            if (licenseNumber != dto.LicenseNumber.ToString())
            {
                return BadRequest();
            }

            try
            {
                var updatedStaff = await _staffService.UpdateAsync(dto);

                if (updatedStaff == null)
                {
                    return NotFound();
                }

                return Ok(updatedStaff);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{licenseNumber}")]
        public async Task<ActionResult<StaffDTO>> SoftDeleteStaff(string licenseNumber)
        {
            try
            {
                var deletedStaff = await _staffService.DeleteAsync(new LicenseNumber(licenseNumber));

                if (deletedStaff == null)
                {
                    return NotFound();
                }

                return Ok(deletedStaff);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
