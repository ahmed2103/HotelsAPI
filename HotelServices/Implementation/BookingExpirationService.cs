using DataAcess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelServices.Implementation
{
    public class BookingExpirationService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public BookingExpirationService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();

                var expiredBookings = await dbContext.Bookings
                    .Where(b => b.Status == BookingStatus.Pending &&
                                b.ExpiresAt != null &&
                                b.ExpiresAt < DateTime.Now)
                    .ToListAsync();

                foreach (var booking in expiredBookings)
                {
                    booking.Status = BookingStatus.Cancelled;
                }

                if (expiredBookings.Any())
                {
                    await dbContext.SaveChangesAsync();
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); 
            }
        }
    }

}
