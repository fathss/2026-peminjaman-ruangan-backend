using PeminjamanRuanganAPI.DTO;

namespace PeminjamanRuanganAPI.Services
{
    public interface IRoomBookingService
    {
        Task<IEnumerable<RoomBookingResponseDto>> GetAllAsync();

        Task<RoomBookingResponseDto?> GetByIdAsync(int id);

        Task<RoomBookingResponseDto> CreateAsync(CreateRoomBookingDto dto);

        Task<bool> UpdateAsync(int id, UpdateRoomBookingDto dto);

        Task<bool> DeleteAsync(int id);
    }
}