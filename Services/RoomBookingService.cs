using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PeminjamanRuanganAPI.Constants;
using PeminjamanRuanganAPI.Data;
using PeminjamanRuanganAPI.DTO;
using PeminjamanRuanganAPI.Models;
using PeminjamanRuanganAPI.Mappings;

namespace PeminjamanRuanganAPI.Services
{
    public class RoomBookingService : IRoomBookingService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RoomBookingService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoomBookingResponseDto>> GetAllAsync(int? userId = null, string? role = null)
        {
            var query = _context.RoomBookings.AsQueryable();

            if (role != "Admin" && userId.HasValue)
            {
                query = query.Where(b => b.UserId == userId.Value);
            }

            var bookings = await query
                .Include(b => b.Room)
                .Include(b => b.User)
                .ToListAsync();

            return _mapper.Map<IEnumerable<RoomBookingResponseDto>>(bookings);
        }

        public async Task<RoomBookingResponseDto?> GetByIdAsync(int bookingId)
        {
            var roomBooking = await _context.RoomBookings
                .Include(r => r.Room)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == bookingId);

            if (roomBooking == null) return null;

            return _mapper.Map<RoomBookingResponseDto>(roomBooking);
        }

        public async Task<RoomBookingResponseDto> CreateAsync(CreateRoomBookingDto dto, int userId)
        {
            dto.StartTime = dto.StartTime.ToUniversalTime();
            dto.EndTime = dto.EndTime.ToUniversalTime();

            await ValidateBookingAsync(dto.RoomId, dto.StartTime, dto.EndTime);

            var booking = _mapper.Map<RoomBooking>(dto);

            booking.UserId = userId;
            booking.Status = BookingStatuses.Pending;

            _context.RoomBookings.Add(booking);
            await _context.SaveChangesAsync();

            var result = await _context.RoomBookings
                .Include(r => r.Room).Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == booking.Id);

            return _mapper.Map<RoomBookingResponseDto>(result);
        }

        public async Task<bool> UpdateAsync(int bookingId, UpdateRoomBookingDto dto, int userId, string userRole)
        {
            dto.StartTime = dto.StartTime.ToUniversalTime();
            dto.EndTime = dto.EndTime.ToUniversalTime();
            
            var roomBooking = await _context.RoomBookings.FindAsync(bookingId);
            if (roomBooking == null) return false;

            if (userRole != "Admin" && roomBooking.UserId != userId)
                throw new Exception(ErrorMessages.UnauthorizedEditAccess);

            if (roomBooking.Status != BookingStatuses.Pending)
                throw new Exception(ErrorMessages.CannotEditNonPending);

            await ValidateBookingAsync(roomBooking.RoomId, dto.StartTime, dto.EndTime, bookingId); 

            _mapper.Map(dto, roomBooking);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var roomBooking = await _context.RoomBookings.FindAsync(id);
            if (roomBooking == null) return false;

            if (roomBooking.Status == BookingStatuses.OnGoing)
                throw new Exception(ErrorMessages.CannotDeleteOngoing);

            _context.RoomBookings.Remove(roomBooking);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task ChangeStatusAsync(RoomBooking roomBooking, string newStatus, int changedByUserId)
        {
            var oldStatus = roomBooking.Status;

            roomBooking.Status = newStatus;

            _context.BookingStatusHistories.Add(new BookingStatusHistory
            {
                RoomBookingId = roomBooking.Id,
                OldStatus = oldStatus.ToString(),
                NewStatus = newStatus.ToString(),
                ChangedByUserId = changedByUserId,
                ChangedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ApproveAsync(int id, int changedByUserId)
        {
            var roomBooking = await _context.RoomBookings.FindAsync(id);
            if (roomBooking == null) return false;

            switch (roomBooking.Status)
            {
                case BookingStatuses.Approved:
                case BookingStatuses.OnGoing:
                    throw new Exception(ErrorMessages.AlreadyApproved);
                case BookingStatuses.Cancelled:
                    throw new Exception(ErrorMessages.CannotApproveCancelled);
                case BookingStatuses.Completed:
                    throw new Exception(ErrorMessages.CannotApproveCompleted);
            }

            await ChangeStatusAsync(roomBooking, BookingStatuses.Approved, changedByUserId);
            return true;
        }

        public async Task<bool> RejectAsync(int id, int changedByUserId)
        {
            var roomBooking = await _context.RoomBookings.FindAsync(id);
            if (roomBooking == null) return false;

            switch (roomBooking.Status)
            {
                case BookingStatuses.Rejected:
                    throw new Exception(ErrorMessages.AlreadyRejected);
                case BookingStatuses.Cancelled:
                    throw new Exception(ErrorMessages.CannotRejectCancelled);
                case BookingStatuses.Completed:
                    throw new Exception(ErrorMessages.CannotRejectCompleted);
                case BookingStatuses.OnGoing:
                    throw new Exception(ErrorMessages.CannotRejectOngoing);
            }

            await ChangeStatusAsync(roomBooking, BookingStatuses.Rejected, changedByUserId);
            return true;
        }

        public async Task<bool> CancelAsync(int id, int changedByUserId, string userRole)
        {
            var roomBooking = await _context.RoomBookings.FindAsync(id);
            if (roomBooking == null) return false;

            if (userRole != "Admin" && roomBooking.UserId != changedByUserId)
            {
                throw new Exception(ErrorMessages.UnauthorizedCancel);
            }

            switch (roomBooking.Status)
            {
                case BookingStatuses.Cancelled:
                    throw new Exception(ErrorMessages.AlreadyCancelled);
                case BookingStatuses.Rejected:
                    throw new Exception(ErrorMessages.CannotCancelRejected);
                case BookingStatuses.Completed:
                    throw new Exception(ErrorMessages.CannotCancelCompleted);
                case BookingStatuses.OnGoing:
                    throw new Exception(ErrorMessages.CannotCancelOngoing);
            }

            await ChangeStatusAsync(roomBooking, BookingStatuses.Cancelled, changedByUserId);
            return true;
        }

        private async Task ValidateBookingAsync(int roomId, DateTime start, DateTime end, int? excludeId = null)
        {
            var room = await _context.Rooms.FindAsync(roomId);
    
            if (room == null) throw new Exception(ErrorMessages.RoomNotFound);

            if (!room.IsActive) throw new Exception(ErrorMessages.RoomInactive);

            if (start < DateTime.UtcNow) throw new Exception(ErrorMessages.StartTimeInPast);

            if (start >= end) throw new Exception(ErrorMessages.InvalidTimeRange);

            var isOverlapping = await _context.RoomBookings
                .AnyAsync(b => b.RoomId == roomId && 
                            b.Id != excludeId && 
                            b.Status != BookingStatuses.Cancelled && 
                            b.Status != BookingStatuses.Rejected &&
                            b.Status != BookingStatuses.Completed &&
                            start < b.EndTime && 
                            end > b.StartTime);

            if (isOverlapping) throw new Exception(ErrorMessages.BookingConflict);
        }
    }
}