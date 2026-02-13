using System.ComponentModel.DataAnnotations;

namespace PeminjamanRuanganAPI.DTO
{
    public class CreateRoomDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Location { get; set; } = null!;

        [Range(1, 1000)]
        public int Capacity { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}