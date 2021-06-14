using Lekkerbek12Gip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Interfaces
{
    public interface IReviewService : IEntityRepositoryBase<Review>
    {
        Task<Review> AddReview(Review review);
        Task DeleteReview(int? id);
        Task<Review> UpdateReview(Review review);
        Task<IEnumerable<Review>> GetAllReviews();
        Task<Review> GetReviewById(int? id);
        bool ReviewExists(int id);
    }
}
