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

        public async Task<IEnumerable<RoomBookingResponseDto>> GetAllAsync()
        {
            var roomBookings = await _context.RoomBookings
                .Include(r => r.Room)
                .Include(r => r.User)
                .ToListAsync();

            return _mapper.Map<IEnumerable<RoomBookingResponseDto>>(roomBookings);
        }

        public async Task<RoomBookingResponseDto?> GetByIdAsync(int id)
        {
            var roomBooking = await _context.RoomBookings
                .Include(r => r.Room)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            return roomBooking != null ? _mapper.Map<RoomBookingResponseDto>(roomBooking) : null;
        }

        public async Task<RoomBookingResponseDto> CreateAsync(CreateRoomBookingDto dto)
        {
            await ValidateBookingAsync(dto.RoomId, dto.StartTime, dto.EndTime);

            var booking = _mapper.Map<RoomBooking>(dto);
            booking.UserId = 2; // TODO: Ambil UserId dari Auth

            _context.RoomBookings.Add(booking);
            await _context.SaveChangesAsync();

            var result = await _context.RoomBookings
                .Include(r => r.Room).Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == booking.Id);

            return _mapper.Map<RoomBookingResponseDto>(result);
        }

        public async Task<bool> UpdateAsync(int id, UpdateRoomBookingDto dto)
        {
            var roomBooking = await _context.RoomBookings.FindAsync(id);
            if (roomBooking == null) return false;

            if (roomBooking.Status == BookingStatuses.OnGoing)
                throw new Exception(ErrorMessages.CannotEditOngoing);
            
            if (roomBooking.Status == BookingStatuses.Completed)
                throw new Exception(ErrorMessages.CannotEditCompleted);

            await ValidateBookingAsync(roomBooking.RoomId, dto.StartTime, dto.EndTime, id); 

            _mapper.Map(dto, roomBooking);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var roomBooking = await _context.RoomBookings.FindAsync(id);
            if (roomBooking == null) return false;

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
                ChangedByUserId = changedByUserId
            });

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ApproveAsync(int id, int changedByUserId)
        {
            var roomBooking = await _context.RoomBookings.FindAsync(id);
            if (roomBooking == null) return false;

            var computedStatus = GetComputedStatus(roomBooking);

            if (computedStatus == BookingStatuses.Cancelled)
                throw new Exception(ErrorMessages.CannotApproveCancelled);

            if (computedStatus == BookingStatuses.Completed)
                throw new Exception(ErrorMessages.CannotApproveCompleted);

            if (computedStatus == BookingStatuses.Approved || computedStatus == BookingStatuses.OnGoing)
                throw new Exception(ErrorMessages.AlreadyApproved);

            await ChangeStatusAsync(roomBooking, BookingStatuses.Approved, changedByUserId);
            return true;
        }

        public async Task<bool> RejectAsync(int id, int changedByUserId)
        {
            var roomBooking = await _context.RoomBookings.FindAsync(id);
            if (roomBooking == null) return false;

            var computedStatus = GetComputedStatus(roomBooking);

            if (computedStatus == BookingStatuses.Cancelled)
                throw new Exception(ErrorMessages.CannotRejectCancelled);

            if (computedStatus == BookingStatuses.Completed)
                throw new Exception(ErrorMessages.CannotRejectCompleted);
            
            if (computedStatus == BookingStatuses.OnGoing)
                throw new Exception(ErrorMessages.CannotRejectOngoing);

            if (computedStatus == BookingStatuses.Rejected)
                throw new Exception(ErrorMessages.AlreadyRejected);

            await ChangeStatusAsync(roomBooking, BookingStatuses.Rejected, changedByUserId);
            return true;
        }

        public async Task<bool> CancelAsync(int id, int changedByUserId)
        {
            var roomBooking = await _context.RoomBookings.FindAsync(id);
            if (roomBooking == null) return false;

            var computedStatus = GetComputedStatus(roomBooking);

            if (computedStatus == BookingStatuses.Cancelled)
                throw new Exception(ErrorMessages.AlreadyCancelled);

            if (computedStatus == BookingStatuses.Completed)
                throw new Exception(ErrorMessages.CannotCancelCompleted);

            if (computedStatus == BookingStatuses.OnGoing)
                throw new Exception(ErrorMessages.CannotCancelOngoing);

            await ChangeStatusAsync(roomBooking, BookingStatuses.Cancelled, changedByUserId);
            return true;
        }

        private string GetComputedStatus(RoomBooking booking)
        {
            if (booking.Status == BookingStatuses.Approved)
            {
                var now = DateTime.Now;

                if (now >= booking.StartTime && now < booking.EndTime)
                    return BookingStatuses.OnGoing;

                if (now >= booking.EndTime)
                    return BookingStatuses.Completed;
            }

            return booking.Status;
        }

        private async Task ValidateBookingAsync(int roomId, DateTime start, DateTime end, int? excludeId = null)
        {
            var room = await _context.Rooms.FindAsync(roomId);
    
            if (room == null) throw new Exception(ErrorMessages.RoomNotFound);

            if (!room.IsActive) throw new Exception(ErrorMessages.RoomInactive);

            if (start >= end) throw new Exception(ErrorMessages.InvalidTimeRange);

            var isOverlapping = await _context.RoomBookings
                .AnyAsync(b => b.RoomId == roomId && 
                            b.Id != excludeId && 
                            b.Status != BookingStatuses.Cancelled && 
                            b.Status != BookingStatuses.Rejected &&
                            start < b.EndTime && 
                            end > b.StartTime);

            if (isOverlapping) throw new Exception(ErrorMessages.BookingConflict);
        }
    }
}