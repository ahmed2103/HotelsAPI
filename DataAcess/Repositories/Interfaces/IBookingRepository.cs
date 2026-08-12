using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.Interfaces
{
    public interface IBookingRepository: IGenericRepository<Booking>
    {
        Task<IEnumerable<Booking>> GetBookingsByGuestIdAsync(string guestId);
        Task<IEnumerable<Booking>> GetActiveBookingsAsync();
        Task<IEnumerable<Booking>> GetBookingsByRoomIdAsyn(int roomId);
        Task<IEnumerable<Booking>> GetLatestBookings();
        Task<IEnumerable<Booking>> GetAllAsync();



    }
}
