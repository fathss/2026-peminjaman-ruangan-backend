using PeminjamanRuanganAPI.Constants;
using PeminjamanRuanganAPI.Common;
using PeminjamanRuanganAPI.Models;

namespace PeminjamanRuanganAPI.Models
{
    public class RoomBooking : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int RoomId { get; set; }
        public Room Room { get; set; } = null!;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Purpose { get; set; } = null!;

        public string Status { get; set; } 
            = BookingStatuses.Pending;

        public DateTime CreatedAt { get; set; }
            = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public ICollection<BookingStatusHistory> StatusHistories
            { get; set; } = new List<BookingStatusHistory>();
    }
}
