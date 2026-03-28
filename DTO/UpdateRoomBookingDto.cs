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
        [MinLength(10, ErrorMessage = "Tujuan peminjaman minimal harus 10 karakter.")]
        [MaxLength(255, ErrorMessage = "Tujuan peminjaman maksimal 255 karakter.")]
        public string Purpose { get; set; } = null!;
    }
}