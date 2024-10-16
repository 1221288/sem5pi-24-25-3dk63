using Microsoft.AspNetCore.Mvc;
using DDDSample1.Domain.Specialization;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using DDDSample1.Domain.Staff;

namespace DDDSample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationsController : ControllerBase
    {
        private readonly SpecializationService _service;

        public SpecializationsController(SpecializationService service)
        {
            _service = service;
        }

        // GET: api/Specializations
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<SpecializationDTO>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        // GET: api/Specializations/{licenseNumber}
        [HttpGet("{licenseNumber}")]
        public async Task<ActionResult<SpecializationDTO>> GetByLicenseNumber(string licenseNumber)
        {
            var specialization = await _service.GetByLicenseNumberAsync(new LicenseNumber(licenseNumber));

            if (specialization == null)
            {
                return NotFound();
            }

            return specialization;
        }

        // POST: api/Specializations
        [HttpPost]
        public async Task<ActionResult<SpecializationDTO>> Create(CreatingSpecializationDTO dto)
        {
            var specialization = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetByLicenseNumber), new { licenseNumber = specialization.LicenseNumber.Value }, specialization);
        }

        // PUT: api/Specializations/{licenseNumber}
        [HttpPut("{licenseNumber}")]
        public async Task<ActionResult<SpecializationDTO>> Update(string licenseNumber, SpecializationDTO dto)
        {
            if (licenseNumber != dto.LicenseNumber.Value)
            {
                return BadRequest();
            }

            try
            {
                var specialization = await _service.UpdateAsync(dto);

                if (specialization == null)
                {
                    return NotFound();
                }

                return Ok(specialization);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE: api/Specializations/{licenseNumber}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{licenseNumber}")]
        public async Task<ActionResult<SpecializationDTO>> SoftDelete(string licenseNumber)
        {
            try
            {
                var specialization = await _service.DeleteAsync(new LicenseNumber(licenseNumber));

                if (specialization == null)
                {
                    return NotFound();
                }

                return Ok(specialization);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
