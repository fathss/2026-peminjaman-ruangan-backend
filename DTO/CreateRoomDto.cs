using System.ComponentModel.DataAnnotations;

namespace PeminjamanRuanganAPI.DTO
{
    public class CreateRoomDto
    {
        [Required]
        [MinLength(6, ErrorMessage = "Nama ruangan minimal harus 6 karakter.")]
        [MaxLength(100, ErrorMessage = "Nama ruangan maksimal 100 karakter.")]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(10, ErrorMessage = "Lokasi minimal harus 10 karakter.")]
        [MaxLength(100, ErrorMessage = "Lokasi maksimal 100 karakter.")]
        public string Location { get; set; } = null!;

        [Required]
        [Range(20, 1000, ErrorMessage = "Kapasitas harus diantara 20 dan 1000")]
        public int Capacity { get; set; }

        [Required]
        [MaxLength(500)]
        public string? Description { get; set; }
    }
}