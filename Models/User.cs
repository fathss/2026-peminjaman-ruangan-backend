using PeminjamanRuanganAPI.Common;
using PeminjamanRuanganAPI.Models;

namespace PeminjamanRuanganAPI.Models
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string Role { get; set; } = "User";

        public ICollection<RoomBooking> RoomBookings { get; set; }
            = new List<RoomBooking>();
    }
}