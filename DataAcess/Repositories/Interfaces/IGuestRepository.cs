using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.Interfaces
{
    public interface IGuestRepository: IGenericRepository<ApplicationUser>
    {
        Task<ApplicationUser?> GetGuestWithBookingsAsync(string UserID);
        Task<ApplicationUser?> GetUserById(string userid);

    }
}
