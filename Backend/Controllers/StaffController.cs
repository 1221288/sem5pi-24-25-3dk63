using Microsoft.AspNetCore.Mvc;
using DDDSample1.Domain.Staff;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using Backend.Domain.Staff.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDSample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly StaffService _staffService;

        public StaffController(StaffService staffService)
        {
            _staffService = staffService;
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
        [Authorize(Roles="Admin")]
        public async Task<ActionResult<StaffDTO>> CreateStaffAsync(CreatingStaffDTO staffDto)
        {
            try
            {
                var createdStaff = await _staffService.CreateStaffWithUserAsync(staffDto);
                return CreatedAtAction(nameof(GetStaffByLicenseNumber), new { licenseNumber = createdStaff.LicenseNumber }, createdStaff);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{licenseNumber}")]
        public async Task<ActionResult<StaffDTO>> UpdateStaff(string licenseNumber, StaffDTO dto)
        {
            if (licenseNumber != dto.LicenseNumber.ToString())
            {
                return BadRequest("License number mismatch.");
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

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<StaffDTO>>> SearchStaffAsync([FromQuery] string? name = null, [FromQuery] string? email = null, [FromQuery] string? specialization = null)
        {
            var staffList = await _staffService.SearchStaffAsync(name, email, specialization);
            return Ok(staffList);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{licenseNumber}")]
        public async Task<ActionResult<StaffDTO>> DeactivateAsync(string licenseNumber)
        {
            try
            {
                var operation = await _staffService.DeactivateAsync(new LicenseNumber(licenseNumber));

                if (operation == null)
                {
                    return NotFound();
                }

                return Ok(operation);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
