using PeminjamanRuanganAPI.DTO;

namespace PeminjamanRuanganAPI.Services
{
    public interface IStatusHistoryService
    {
        Task<IEnumerable<StatusHistoryDto>> GetByBookingIdAsync(int bookingId);
    }
}