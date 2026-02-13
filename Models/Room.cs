using PeminjamanRuanganAPI.Common;
using PeminjamanRuanganAPI.Models;

namespace PeminjamanRuanganAPI.Models
{
    public class Room : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public int Capacity { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<RoomBooking> RoomBookings { get; set; }
            = new List<RoomBooking>();
    }
}
