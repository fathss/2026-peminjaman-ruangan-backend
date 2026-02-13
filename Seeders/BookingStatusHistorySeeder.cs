using Microsoft.EntityFrameworkCore;
using PeminjamanRuanganAPI.Models;
using PeminjamanRuanganAPI.Constants;

namespace PeminjamanRuanganAPI.Seeders
{
    public class BookingStatusHistorySeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingStatusHistory>().HasData(
                new BookingStatusHistory
                {
                    Id = 1,
                    RoomBookingId = 1,
                    OldStatus = BookingStatuses.Pending,
                    NewStatus = BookingStatuses.Pending,
                    ChangedAt = new DateTime(2026, 1, 1, 9, 5, 0),
                    ChangedByUserId = 2,
                    IsDeleted = false,
                    DeletedAt = null
                },
                new BookingStatusHistory
                {
                    Id = 2,
                    RoomBookingId = 2,
                    OldStatus = BookingStatuses.Pending,
                    NewStatus = BookingStatuses.Pending,
                    ChangedAt = new DateTime(2026, 1, 1, 9, 5, 0),
                    ChangedByUserId = 2,
                    IsDeleted = false,
                    DeletedAt = null
                }
            );
        }
    }
}