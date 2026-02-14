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
        private readonly IStatusHistoryService _historyService;

        public RoomBookingsController(IRoomBookingService service, IStatusHistoryService historyService)
        {
            _service = service;
            _historyService = historyService;
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

        // Put: api/roombookings/{id}/approve
        [HttpPut("{id}/approve")]
        public async Task<IActionResult> Approve(int id)
        {
            try
            {
                var adminId = 1; // TODO: Ambil UserId dari Auth

                var success = await _service.ApproveAsync(id, adminId);

                if (!success) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Put: api/roombookings/{id}/reject
        [HttpPut("{id}/reject")]
        public async Task<IActionResult> Reject(int id)
        {
            try
            {
                var adminId = 1; // TODO: Ambil UserId dari Auth

                var success = await _service.RejectAsync(id, adminId);

                if (!success) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Put: api/roombookings/{id}/cancel
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
                var userId = 2; // TODO: Ambil UserId dari Auth

                var success = await _service.CancelAsync(id, userId);

                if (!success) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Get: api/roombookings/{id}/histories
        [HttpGet("{id}/histories")]
        public async Task<ActionResult<IEnumerable<StatusHistoryDto>>> GetHistoryById(int id)
        {
            var bookingExists = await _service.GetByIdAsync(id);
            if (bookingExists == null)            
            {
                return NotFound();
            }

            var histories = await _historyService.GetByBookingIdAsync(id);

            return Ok(histories ?? new List<StatusHistoryDto>());
        }
    }
}