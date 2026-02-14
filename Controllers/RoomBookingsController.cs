using Microsoft.AspNetCore.Mvc;
using PeminjamanRuanganAPI.DTO;
using PeminjamanRuanganAPI.Services;

namespace PeminjamanRuanganAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomBookingsController : ControllerBase
    {
        private readonly IRoomBookingService _service;

        public RoomBookingsController(IRoomBookingService service)
        {
            _service = service;
        }

        // Get: api/roombookings
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _service.GetAllAsync();
            return Ok(bookings);
        }

        // Get: api/roombookings/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var booking = await _service.GetByIdAsync(id);
            if (booking == null) return NotFound();

            return Ok(booking);
        }

        // Post: api/roombookings
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoomBookingDto dto)
        {
            try
            {
                var createdBooking = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdBooking.Id }, createdBooking);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Put: api/roombookings/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateRoomBookingDto dto)
        {
            try
            {
                var success = await _service.UpdateAsync(id, dto);
                if (!success) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Delete: api/roombookings/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }   
    }
}