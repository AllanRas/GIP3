using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Concrete
{
    public class ReviewService : RepositoryBase<Review>, IReviewService
    {
        private readonly LekkerbekContext _context;
        private readonly IKlantsService _klantsService;

        public ReviewService(LekkerbekContext context, IKlantsService klantsService) : base(context)
        {
            _context = context;
            _klantsService = klantsService;
        }

        public async Task<Review> AddReview(Review review)
        {
            var klant = _klantsService.Get(x => x.KlantId == review.KlantId);

            if (klant != null)
            {
                review.TimeOfReview = DateTime.Now;
            }
            await Add(review);
            return review;
        }

        public async Task DeleteReview(int? id)
        {
            var review = _context.Reviews
                .Include(r => r.Klant)
                .FirstOrDefaultAsync(m => m.ReviewId == id).Result;
            await Delete(review);
        }

        public async Task<IEnumerable<Review>> GetAllReviews()
        {
            var reviewList = _context.Reviews
                .Include(x => x.Klant)
                .OrderBy(x => x.TimeOfReview);
            return await reviewList.ToListAsync();
        }

        public async Task<Review> GetReviewById(int? id)
        {
            return await _context.Reviews
                .Include(r => r.Klant)
                .FirstOrDefaultAsync(m => m.ReviewId == id);
        }

        public async Task<Review> UpdateReview(Review review)
        {
            await Update(review);
            return review;
        }
        public bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.ReviewId == id);
        }
    }
}
