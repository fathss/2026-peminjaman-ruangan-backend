using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PeminjamanRuanganAPI.Data;
using PeminjamanRuanganAPI.DTO;
using PeminjamanRuanganAPI.Models;
using PeminjamanRuanganAPI.Mappings;

namespace PeminjamanRuanganAPI.Services
{
    public class StatusHistoryService : IStatusHistoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public StatusHistoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StatusHistoryDto>> GetByBookingIdAsync(int bookingId)
        {
            var results = await _context.BookingStatusHistories
                .Where(h => h.RoomBookingId == bookingId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<StatusHistoryDto>>(results);
        }
    }
}