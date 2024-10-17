using Microsoft.AspNetCore.Mvc;
using DDDSample1.Domain.Specialization;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Authorization;

namespace DDDSample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly SpecializationService _specializationService;

        public SpecializationController(SpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecializationDTO>>> GetAllSpecializations()
        {
            return await _specializationService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SpecializationDTO>> GetSpecializationById(Guid id)
        {
            var specialization = await _specializationService.GetBySpecializationIdAsync(new SpecializationId(id));

            if (specialization == null)
            {
                return NotFound();
            }

            return specialization;
        }

        [HttpPost]
        public async Task<ActionResult<SpecializationDTO>> CreateSpecialization(CreatingSpecializationDTO dto)
        {
            var specialization = await _specializationService.AddAsync(dto);
            return CreatedAtAction(nameof(GetSpecializationById), new { id = specialization.Id }, specialization);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SpecializationDTO>> UpdateSpecialization(Guid id, SpecializationDTO dto)
        {
            if (id != dto.Id.AsGuid())
            {
                return BadRequest();
            }

            try
            {
                var specialization = await _specializationService.UpdateAsync(dto);

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

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<SpecializationDTO>> DeleteSpecialization(Guid id)
        {
            try
            {
                var specialization = await _specializationService.DeleteAsync(new SpecializationId(id));

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
