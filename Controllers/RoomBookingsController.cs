using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PeminjamanRuanganAPI.DTO;
using PeminjamanRuanganAPI.Services;

namespace PeminjamanRuanganAPI.Controllers
{
    [ApiController]
    [Authorize]
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
        public async Task<ActionResult<IEnumerable<RoomBookingResponseDto>>> GetAll()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var role = User.FindFirstValue(ClaimTypes.Role)!;

            var results = await _service.GetAllAsync(userId, role);
            return Ok(results);
        }

        // Get: api/roombookings/{id}
        [HttpGet("{id:int}")]
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
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            try
            {
                var createdBooking = await _service.CreateAsync(dto, userId);
                return CreatedAtAction(nameof(GetById), new { id = createdBooking.Id }, createdBooking);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Put: api/roombookings/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateRoomBookingDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var role = User.FindFirstValue(ClaimTypes.Role)!;

            try
            {
                var success = await _service.UpdateAsync(id, dto, userId, role);
                if (!success) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Delete: api/roombookings/{id}
        [HttpDelete("{id:int}")]
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
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}/approve")]
        public async Task<IActionResult> Approve(int id)
        {
            try
            {
                var adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

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
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}/reject")]
        public async Task<IActionResult> Reject(int id)
        {
            try
            {
                var adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var success = await _service.RejectAsync(id, adminId);

                if (!success) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Put: api/roombookings/{id}/complete
        [HttpPut("{id:int}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var role = User.FindFirstValue(ClaimTypes.Role)!;

            try
            {
                var success = await _service.CompleteAsync(id, userId, role);
                if (!success) return NotFound();
                return Ok(new { message = "Booking berhasil diselesaikan" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Put: api/roombookings/{id}/cancel
        [HttpPut("{id:int}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var role = User.FindFirstValue(ClaimTypes.Role)!;

            try
            {
                var success = await _service.CancelAsync(id, userId, role);
                if (!success) return NotFound();
                return Ok(new { message = "Booking berhasil dibatalkan" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Get: api/roombookings/{id}/histories
        [HttpGet("{id:int}/histories")]
        public async Task<ActionResult<IEnumerable<StatusHistoryDto>>> GetHistoryById(int id)
        {
            try
            {
                var histories = await _historyService.GetByBookingIdAsync(id);

                return Ok(histories ?? new List<StatusHistoryDto>());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}