using PeminjamanRuanganAPI.Constants;
using PeminjamanRuanganAPI.Common;
using PeminjamanRuanganAPI.Models;

namespace PeminjamanRuanganAPI.Models
{
    public class BookingStatusHistory : BaseEntity
    {
        public int RoomBookingId { get; set; }
        public RoomBooking RoomBooking { get; set; } = null!;

        public string OldStatus { get; set; } = null!;

        public string NewStatus { get; set; } = null!;

        public DateTime ChangedAt { get; set; }
            = DateTime.Now;

        public int? ChangedByUserId { get; set; }
    }
}
