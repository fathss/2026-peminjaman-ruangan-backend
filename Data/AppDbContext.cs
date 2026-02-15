using Microsoft.EntityFrameworkCore;
using PeminjamanRuanganAPI.Common;
using PeminjamanRuanganAPI.Models;
using PeminjamanRuanganAPI.Seeders;

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
            modelBuilder.Entity<User>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<Room>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<RoomBooking>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<BookingStatusHistory>()
                .HasQueryFilter(e => !e.IsDeleted);

            base.OnModelCreating(modelBuilder);

            UserSeeders.Seed(modelBuilder);
            RoomSeeder.Seed(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedAt = DateTime.Now;

                    if (entry.Entity is Room room)
                    {
                        room.IsActive = false;
                    }
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}

