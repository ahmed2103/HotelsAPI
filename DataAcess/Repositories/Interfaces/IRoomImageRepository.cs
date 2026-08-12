using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.Interfaces
{
    public interface IRoomImageRepository : IGenericRepository<RoomImage>
    {
        Task<IEnumerable<RoomImage>> GetByRoomIdAsync(int  roomId);
    }
}
