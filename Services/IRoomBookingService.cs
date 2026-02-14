using PeminjamanRuanganAPI.DTO;
using PeminjamanRuanganAPI.Models;

namespace PeminjamanRuanganAPI.Services
{
    public interface IRoomBookingService
    {
        Task<IEnumerable<RoomBookingResponseDto>> GetAllAsync();

        Task<RoomBookingResponseDto?> GetByIdAsync(int id);

        Task<RoomBookingResponseDto> CreateAsync(CreateRoomBookingDto dto);

        Task<bool> UpdateAsync(int id, UpdateRoomBookingDto dto);

        Task<bool> DeleteAsync(int id);

        Task<bool> ApproveAsync(int id, int changedByUserId);

        Task<bool> RejectAsync(int id, int changedByUserId);

        Task<bool> CancelAsync(int id, int changedByUserId);
    }
}