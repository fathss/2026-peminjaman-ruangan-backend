using PeminjamanRuanganAPI.Models;
using PeminjamanRuanganAPI.Constants;

namespace PeminjamanRuanganAPI.Models
{
    public class BookingStatusHistory
    {
        public int Id { get; set; }

        public int RoomBookingId { get; set; }
        public RoomBooking RoomBooking { get; set; } = null!;

        public string OldStatus { get; set; } = null!;

        public string NewStatus { get; set; } = null!;

        public DateTime ChangedAt { get; set; }
            = DateTime.UtcNow;

        public int ChangedByUserId { get; set; }
    }
}
