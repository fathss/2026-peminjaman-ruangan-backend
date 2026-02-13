using Microsoft.EntityFrameworkCore;
using PeminjamanRuanganAPI.Models;

namespace PeminjamanRuanganAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        public DbSet<User> Users => Set<User>();

        public DbSet<Room> Rooms => Set<Room>();

        public DbSet<RoomBooking> RoomBookings => Set<RoomBooking>();

        public DbSet<BookingStatusHistory> BookingStatusHistories => Set<BookingStatusHistory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomBooking>()
                .HasQueryFilter(b => !b.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }
    }
}

