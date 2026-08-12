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
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly HotelDbContext _dbcontext;

        public ReviewRepository(HotelDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task AddOrUpdateReviewAsync(string userid, Review review)
        {
            int hotelId = 1;
            var existingReview = await _dbcontext.Reviews.FirstOrDefaultAsync(r => r.HotelId == hotelId && r.UserId == userid);
            if (existingReview != null)
            {
                // Update existing review
                existingReview.Rating = review.Rating;
                existingReview.Comment = review.Comment;
                existingReview.CreatedAt = DateTime.UtcNow; // Update timestamp
                
            }
            else
            {
                // Add new review
                review.UserId = userid;
                review.HotelId = hotelId;
                review.CreatedAt = DateTime.UtcNow;
                await AddAsync(review);
            }
        }

        public async Task<double> GetHotelRatingAsync()
        {
           int hotelId = 1;
            var ratings = await _dbcontext.Reviews
                .Where(r => r.HotelId == hotelId)
                .Select(r => r.Rating)
                .ToListAsync();

            if (!ratings.Any())
                return 0; 

            return Math.Round(ratings.Average(),1);
        }
        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _dbcontext.Reviews.Include(u => u.User).ToListAsync();

        }
    }
}
