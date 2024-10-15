using Microsoft.AspNetCore.Mvc;
using DDDSample1.Domain.OperationsType;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using DDDSample1.Users;
using DDDSample1.OperationsType;

namespace DDDSample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationTypeController : ControllerBase
    {
        private readonly OperationTypeService _service;

        public OperationTypeController(OperationTypeService service)
        {
            _service = service;
        }

        // GET: api/OperationsType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationTypeDTO>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        // GET: api/OperationsType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationTypeDTO>> GetById(Guid id)
        {
            var user = await _service.GetByIdAsync(new OperationTypeId(id));

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/OperationType
        [HttpPost]
        public async Task<ActionResult<OperationTypeDTO>> Create(CreatingOperationTypeDTO dto)
        {
            var user = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        // PUT: api/OperationsType/5
        [HttpPut("{id}")]
        public async Task<ActionResult<OperationTypeDTO>> Update(Guid id, OperationTypeDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            try
            {
                var user = await _service.UpdateAsync(dto);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE: api/OperationsType/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]

        public async Task<ActionResult<OperationTypeDTO>> SoftDelete(Guid id)
        {
            try
            {
                var operation = await _service.DeleteAsync(new OperationTypeId(id));

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
