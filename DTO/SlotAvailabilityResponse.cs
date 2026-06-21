namespace PeminjamanRuanganAPI.DTO
{
    public class SlotAvailabilityResponse
    {
        public string Date { get; set; } = null!;
        public List<TimeSlotDto> Slots { get; set; } = new();
    }

    public class TimeSlotDto
    {
        public string Time { get; set; } = null!;
        public bool Available { get; set; }
    }
}
