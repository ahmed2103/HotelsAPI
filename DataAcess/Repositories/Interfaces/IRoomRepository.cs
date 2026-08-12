using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.Interfaces
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        Task<Room> GetByIdWhithImages(int id);
        Task<IEnumerable<Room>> GetAllWithImages();
        Task<IEnumerable<Room>> GetAvailableRoomsAsync();
    }
}
