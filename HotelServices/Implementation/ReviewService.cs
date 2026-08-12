using AutoMapper;
using DataAcess.Repositories.Implementations;
using DataAcess.Repositories.Interfaces;
using HotelServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DTOS.Reviews;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelServices.Implementation
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepo;

        public ReviewService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IReviewRepository reviewRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _reviewRepo = reviewRepository;
        }

        public async Task CreateReview(string userId, AddReviewsDto dto)
        {
            if (dto.Rating < 1 || dto.Rating > 5)
                throw new ArgumentException("Rating must be between 1 and 5");

            var review = new Review
            {
                Rating = dto.Rating,
                Comment = dto.Comment,
                UserId = userId
            };

            await _reviewRepo.AddOrUpdateReviewAsync(userId, review);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            var review = await _reviewRepo.GetByIdAsync(id);
            if (review == null)
                return false;

            _reviewRepo.Delete(review);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<ReviewResponseDto>> GetAllReviewsAsync()
        {
            var reviews = await _reviewRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<ReviewResponseDto>>(reviews);
        }

        public async Task<ReviewResponseDto> GetReviewById(int id)
        {
            var review = await _reviewRepo.GetByIdAsync(id);
            return _mapper.Map<ReviewResponseDto>(review);
        }
    }

}
