using System.ComponentModel.DataAnnotations;

namespace PeminjamanRuanganAPI.DTO
{
    public class UpdateRoomBookingDto
    {
        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [MaxLength(500)]
        public string Purpose { get; set; } = null!;
    }
}