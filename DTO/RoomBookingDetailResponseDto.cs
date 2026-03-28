namespace PeminjamanRuanganAPI.DTO
{
    public class RoomBookingDetailResponseDto
    {
        public RoomBookingResponseDto Booking { get; set; } = null!;

        public IEnumerable<StatusHistoryDto> Histories { get; set; } = new List<StatusHistoryDto>();
    }
}