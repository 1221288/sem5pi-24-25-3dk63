using Microsoft.AspNetCore.Mvc;
using Backend.Domain.SurgeryRoom;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurgeryRoomController : ControllerBase
    {
        private readonly SurgeryRoomService _service;

        public SurgeryRoomController(SurgeryRoomService service)
        {
            _service = service;
        }

        // GET: api/SurgeryRoom
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurgeryRoomDTO>>> GetAll()
        {

            return await _service.GetAllAsync();
        }

        // GET: api/SurgeryRoom/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SurgeryRoomDTO>> GetById(Guid id)
        {
            var surgeryRoom = await _service.GetByIdAsync(new RoomId(id));

            if (surgeryRoom == null)
            {
                return NotFound();
            }

            return Ok(surgeryRoom);
        }

        // POST: api/SurgeryRoom
        [HttpPost]
        public async Task<ActionResult<SurgeryRoomDTO>> Create(CreatingSurgeryRoomDto dto)
        {
            var surgeryRoom = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = surgeryRoom.RoomId }, surgeryRoom);
        }

        // PUT: api/SurgeryRoom/5
        [HttpPut("{id}")]
        public async Task<ActionResult<SurgeryRoomDTO>> Update(Guid id, SurgeryRoomDTO dto)
        {
            if (id != dto.RoomId)
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateAsync(dto);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE: api/SurgeryRoom/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteAsync(new RoomId(id));
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
