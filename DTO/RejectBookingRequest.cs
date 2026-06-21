using System.ComponentModel.DataAnnotations;

namespace PeminjamanRuanganAPI.DTO
{
    public class RejectBookingRequest
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(500, MinimumLength = 1)]
        public string Reason { get; set; } = null!;
    }
}
