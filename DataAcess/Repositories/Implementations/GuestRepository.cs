using DataAcess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.Implementations
{
    public class GuestRepository : GenericRepository<ApplicationUser>, IGuestRepository
    {
        private readonly HotelDbContext _dbcontext;

        public GuestRepository(HotelDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<ApplicationUser?> GetGuestWithBookingsAsync(string Userid)
        {
            return await _dbcontext.Users
                 .Where(g => g.Id == Userid)
                 .Include(g => g.Bookings)
                 .FirstOrDefaultAsync();
        }

        public async Task<ApplicationUser?> GetUserById(string userid)
        {
            return await _dbcontext.Users
                .FirstOrDefaultAsync(u => u.Id == userid);
        }
        //public async Task<>

    }
}
