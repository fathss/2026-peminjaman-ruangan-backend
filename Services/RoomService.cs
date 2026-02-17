using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PeminjamanRuanganAPI.Constants;
using PeminjamanRuanganAPI.Data;
using PeminjamanRuanganAPI.DTO;
using PeminjamanRuanganAPI.Models;
using PeminjamanRuanganAPI.Mappings;

namespace PeminjamanRuanganAPI.Services
{
    public class RoomService : IRoomService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RoomService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoomResponseDto>> GetAllAsync()
        {
            var rooms = await _context.Rooms.ToListAsync();
            return _mapper.Map<IEnumerable<RoomResponseDto>>(rooms);
        }

        public async Task<RoomResponseDto?> GetByIdAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            return room == null ? null : _mapper.Map<RoomResponseDto>(room);
        }

        public async Task<RoomResponseDto> CreateAsync(CreateRoomDto dto)
        {
            var room = _mapper.Map<Room>(dto);

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return _mapper.Map<RoomResponseDto>(room);
        }

        public async Task<bool> UpdateAsync(int id, UpdateRoomDto dto)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return false;

            _mapper.Map(dto, room);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return false;

            var hasActiveBookings = await _context.RoomBookings
                .AnyAsync(rb => rb.RoomId == id && 
                                rb.Status != BookingStatuses.Rejected && 
                                rb.Status != BookingStatuses.Cancelled && 
                                rb.Status != BookingStatuses.Completed);

            if (hasActiveBookings)
            {
                throw new Exception(ErrorMessages.RoomHasActiveBookings);
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SetActiveAsync(int id, bool isActive)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return false;

            room.IsActive = isActive;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}