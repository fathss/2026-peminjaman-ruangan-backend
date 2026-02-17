using PeminjamanRuanganAPI.DTO;
using PeminjamanRuanganAPI.Models;

namespace PeminjamanRuanganAPI.Services
{
    public interface IRoomBookingService
    {
        Task<IEnumerable<RoomBookingResponseDto>> GetAllAsync(int? userId = null, string? role = null);

        Task<RoomBookingResponseDto?> GetByIdAsync(int bookingId);

        Task<RoomBookingResponseDto> CreateAsync(CreateRoomBookingDto dto, int userId);

        Task<bool> UpdateAsync(int bookingId, UpdateRoomBookingDto dto, int userId, string userRole);

        Task<bool> DeleteAsync(int id);

        Task<bool> ApproveAsync(int id, int changedByUserId);

        Task<bool> RejectAsync(int id, int changedByUserId);

        Task<bool> CompleteAsync(int id, int changedByUserId, string userRole);

        Task<bool> CancelAsync(int id, int changedByUserId, string userRole);
    }
}