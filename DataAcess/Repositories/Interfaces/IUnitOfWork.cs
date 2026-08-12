using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IRoomRepository Rooms { get; }
        IGuestRepository Users { get; }
        IBookingRepository Bookings { get; }
         IRoomImageRepository RoomImages { get; }
 
        Task CompleteAsync();
    }
}
