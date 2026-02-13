using PeminjamanRuanganAPI.Models;
using PeminjamanRuanganAPI.Constants;

namespace PeminjamanRuanganAPI.Models
{
    public class RoomBooking
    {
        public int Id { get; set; }

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
            = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<BookingStatusHistory> StatusHistories
            { get; set; } = new List<BookingStatusHistory>();
    }
}
