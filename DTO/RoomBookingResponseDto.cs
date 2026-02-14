using System.ComponentModel.DataAnnotations;

namespace PeminjamanRuanganAPI.DTO
{
    public class RoomBookingResponseDto
    {
        public int Id { get; set; }

        public int RoomId { get; set; }
        public string RoomName { get; set; } = null!;

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Purpose { get; set; } = null!;

        public string Status { get; set; } = null!;
    }
}