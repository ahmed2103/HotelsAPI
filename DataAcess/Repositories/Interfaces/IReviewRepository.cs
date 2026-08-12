using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.Interfaces
{
    public interface IReviewRepository : IGenericRepository<Models.Entities.Review>
    {
        public  Task<double> GetHotelRatingAsync();
        public Task AddOrUpdateReviewAsync(string userid , Review review);
    }
}
