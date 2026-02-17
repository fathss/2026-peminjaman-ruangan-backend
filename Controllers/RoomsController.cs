using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PeminjamanRuanganAPI.DTO;
using PeminjamanRuanganAPI.Services;

namespace PeminjamanRuanganAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _service;

        public RoomsController(IRoomService service)
        {
            _service = service;
        }

        // Get: api/rooms
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RoomResponseDto>>> GetAll()
        {
            var results = await _service.GetAllAsync();
            return Ok(results);
        }

        // Get: api/rooms/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var room = await _service.GetByIdAsync(id);
            if (room == null) return NotFound();

            return Ok(room);
        }

        // Post: api/rooms
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateRoomDto dto)
        {
            var room = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
        }

        // Put: api/rooms/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateRoomDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound();

            return NoContent();
        }

        // Delete: api/rooms/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _service.DeleteAsync(id);
                if (!success) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}