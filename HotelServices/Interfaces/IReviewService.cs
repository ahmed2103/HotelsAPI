using Models.DTOS.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelServices.Interfaces
{
    public interface IReviewService
    {
        public Task<IEnumerable<ReviewResponseDto>> GetAllReviewsAsync();
        public Task CreateReview(string userid,AddReviewsDto reviewDto);
        public Task<bool> DeleteReviewAsync(int id);
        public Task<ReviewResponseDto> GetReviewById(int id);
        
         
    }
}
