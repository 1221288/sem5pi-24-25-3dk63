using Microsoft.AspNetCore.Mvc;
using DDDSample1.Domain.Staff;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using DDDSample1.Domain.Users;
using DDDSample1.Users;
using DDDSample1.Domain.Specialization;

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
            return await _staffService.GetAllAsync();
        }

        [HttpGet("{licenseNumber}")]
        public async Task<ActionResult<StaffDTO>> GetStaffByLicenseNumber(string licenseNumber)
        {
            var staff = await _staffService.GetByLicenseNumberAsync(new LicenseNumber(licenseNumber));

            if (staff == null)
            {
                return NotFound();
            }

            return staff;
        }

        [HttpPost]
        public async Task<ActionResult<StaffDTO>> CreateStaff(Guid userId, Guid specializationId, CreatingStaffDTO staffDto)
        {
            var user = await _userService.GetByIdAsync(new UserId(userId.ToString()));
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            var specialization = await _specializationService.GetBySpecializationIdAsync(new SpecializationId(specializationId));
            if (specialization == null)
            {
                return NotFound(new { Message = "Specialization not found" });
            }

            staffDto.UserId = new UserId(userId.ToString());
            staffDto.SpecializationId = new SpecializationId(specializationId);

            var staff = await _staffService.AddAsync(staffDto);

            return CreatedAtAction(nameof(GetStaffByLicenseNumber), new { licenseNumber = staff.LicenseNumber.Value }, staff);
        }


        [HttpPut("{licenseNumber}")]
        public async Task<ActionResult<StaffDTO>> UpdateStaff(string licenseNumber, StaffDTO dto)
        {
            if (licenseNumber != dto.LicenseNumber.Value)
            {
                return BadRequest();
            }

            try
            {
                var staff = await _staffService.UpdateAsync(dto);

                if (staff == null)
                {
                    return NotFound();
                }

                return Ok(staff);
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
                var staff = await _staffService.DeleteAsync(new LicenseNumber(licenseNumber));

                if (staff == null)
                {
                    return NotFound();
                }

                return Ok(staff);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
