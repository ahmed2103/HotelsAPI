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
    public class RoomImageRepository : GenericRepository<RoomImage> , IRoomImageRepository
    {
        private readonly HotelDbContext _dbcontext;

        public RoomImageRepository(HotelDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<IEnumerable<RoomImage>> GetByRoomIdAsync(int roomId)
        {
            return await _dbcontext.RoomImages.Where(r=>r.RoomId == roomId).ToListAsync();
        }
    }
}
