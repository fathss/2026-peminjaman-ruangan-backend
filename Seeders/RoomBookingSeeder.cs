using Microsoft.EntityFrameworkCore;
using PeminjamanRuanganAPI.Models;
using PeminjamanRuanganAPI.Constants;

namespace PeminjamanRuanganAPI.Seeders
{
    public class RoomBookingSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomBooking>().HasData(
                new RoomBooking
                {
                    Id = 1,
                    UserId = 2,
                    RoomId = 1,
                    StartTime = new DateTime(2024, 7, 1, 9, 0, 0),
                    EndTime = new DateTime(2024, 7, 1, 11, 0, 0),
                    Purpose = "Diskusi Tugas Akhir",
                    Status = BookingStatuses.Pending,
                    CreatedAt = new DateTime(2026, 1, 1, 9, 0, 0),
                    UpdatedAt = null,
                    IsDeleted = false
                },
                new RoomBooking
                {
                    Id = 2,
                    UserId = 2,
                    RoomId = 3,
                    StartTime = new DateTime(2024, 7, 2, 13, 0, 0),
                    EndTime = new DateTime(2024, 7, 2, 15, 0, 0),
                    Purpose = "Seminar Proyek",
                    Status = BookingStatuses.Pending,
                    CreatedAt = new DateTime(2026, 1, 1, 9, 0, 0),
                    UpdatedAt = null,
                    IsDeleted = false
                }
            );
        }
    }
}