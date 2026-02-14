using Microsoft.EntityFrameworkCore;
using PeminjamanRuanganAPI.Constants;
using PeminjamanRuanganAPI.Data;
using PeminjamanRuanganAPI.Models;

namespace PeminjamanRuanganAPI.Services
{
    public class BookingStatusWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BookingStatusWorker> _logger;

        public BookingStatusWorker(IServiceProvider serviceProvider, ILogger<BookingStatusWorker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var now = DateTime.Now;

                    // Cek Approved -> OnGoing
                    var toOnGoing = await context.RoomBookings
                        .Where(b => b.Status == BookingStatuses.Approved && now >= b.StartTime && now < b.EndTime)
                        .ToListAsync();

                    foreach (var b in toOnGoing) 
                    {
                        b.Status = BookingStatuses.OnGoing;
                        
                        context.BookingStatusHistories.Add(new BookingStatusHistory 
                        {
                            RoomBookingId = b.Id,
                            OldStatus = BookingStatuses.Approved,
                            NewStatus = BookingStatuses.OnGoing,
                            ChangedAt = now,
                            ChangedByUserId = null // System update, tidak ada user yang mengubah
                        });
                    }

                    // Cek OnGoing -> Completed
                    var toCompleted = await context.RoomBookings
                        .Where(b => b.Status == BookingStatuses.OnGoing && now >= b.EndTime)
                        .ToListAsync();

                    foreach (var b in toCompleted) 
                    {
                        b.Status = BookingStatuses.Completed;

                        context.BookingStatusHistories.Add(new BookingStatusHistory 
                        {
                            RoomBookingId = b.Id,
                            OldStatus = BookingStatuses.OnGoing,
                            NewStatus = BookingStatuses.Completed,
                            ChangedAt = now,
                            ChangedByUserId = null // System update, tidak ada user yang mengubah
                        });
                    }

                    if (toOnGoing.Any() || toCompleted.Any())
                    {
                        await context.SaveChangesAsync();
                        _logger.LogInformation("Status & History berhasil diperbarui.");
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}