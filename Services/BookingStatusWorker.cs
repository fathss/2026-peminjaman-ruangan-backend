using Microsoft.EntityFrameworkCore;
using PeminjamanRuanganAPI.Constants;
using PeminjamanRuanganAPI.Data;

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
                _logger.LogInformation("Worker memeriksa pembaruan status booking pada: {time}", DateTimeOffset.Now);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var now = DateTime.Now;

                    // Update Approved -> OnGoing
                    var toOnGoing = await context.RoomBookings
                        .Where(b => b.Status == BookingStatuses.Approved && now >= b.StartTime && now < b.EndTime)
                        .ToListAsync();

                    foreach (var b in toOnGoing) b.Status = BookingStatuses.OnGoing;

                    // Update OnGoing -> Completed
                    var toCompleted = await context.RoomBookings
                        .Where(b => b.Status == BookingStatuses.OnGoing && now >= b.EndTime)
                        .ToListAsync();

                    foreach (var b in toCompleted) b.Status = BookingStatuses.Completed;

                    if (toOnGoing.Any() || toCompleted.Any())
                    {
                        await context.SaveChangesAsync();
                        _logger.LogInformation("Status booking berhasil diperbarui secara otomatis.");
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}