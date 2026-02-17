using PeminjamanRuanganAPI.DTO;

namespace PeminjamanRuanganAPI.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomResponseDto>> GetAllAsync();
        
        Task<RoomResponseDto?> GetByIdAsync(int id);
        
        Task<RoomResponseDto> CreateAsync(CreateRoomDto dto);
        
        Task<bool> UpdateAsync(int id, UpdateRoomDto dto);
        
        Task<bool> DeleteAsync(int id);
    }
}