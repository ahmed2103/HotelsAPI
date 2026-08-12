using DataAcess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HotelDbContext _dbcontext;
        public UnitOfWork(HotelDbContext dbContext )
        {
            _dbcontext = dbContext;

            Rooms = new RoomRepository(_dbcontext);
            Users = new GuestRepository(_dbcontext);
            Bookings = new BookingRepository(_dbcontext);
            RoomImages = new RoomImageRepository(_dbcontext);
        }
        public IRoomRepository Rooms { get; private set; }

        public IBookingRepository Bookings { get; private set; }
        public IGuestRepository Users { get; private set; }

        public IRoomImageRepository RoomImages {  get; private set; }

        public Task CompleteAsync()
        {
            return _dbcontext.SaveChangesAsync();
            
        }

        public void Dispose()
        {
            
            _dbcontext.Dispose();
        }
    }
}
