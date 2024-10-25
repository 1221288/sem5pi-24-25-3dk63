using Microsoft.AspNetCore.Mvc;
using DDDSample1.Domain.OperationsType;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using DDDSample1.Users;
using DDDSample1.OperationsType;
using Backend.Domain.Shared;
using System.Security.Claims;

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

        // GET: api/OperationType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationTypeDTO>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        // GET: api/OperationType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationTypeDTO>> GetById(Guid id)
        {
            var operation = await _service.GetByIdAsync(new OperationTypeId(id));

            if (operation == null)
            {
                return NotFound();
            }

            return operation;
        }

        // POST: api/OperationType
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<String>> Create(CreatingOperationTypeDTO dto)
        {
            try
            {
            var adminEmail = User.FindFirstValue(ClaimTypes.Email);
            var operationType = await _service.AddAsync(dto, adminEmail );

            return "OperationType created successfully!";
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/OperationType/5
        [HttpPut("{id}")]
        public async Task<ActionResult<OperationTypeDTO>> Update(Guid id, OperationTypeDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            try
            {
                var operationType = await _service.UpdateAsync(dto);

                if (operationType == null)
                {
                    return NotFound();
                }

                return Ok(operationType);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE: api/OperationType/{id}
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
        
        // PATCH: api/OperationType/{id}
        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
        public async Task<ActionResult<OperationTypeDTO>> DeactivateAsync(Guid id)
        {
            try
            {
                var adminEmail = User.FindFirstValue(ClaimTypes.Email);

                var operation = await _service.DeactivateAsync(new OperationTypeId(id), adminEmail);

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
