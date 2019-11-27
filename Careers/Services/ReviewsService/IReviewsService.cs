using Careers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Services.ReviewsService
{
    interface IReviewsService
    {
        Task InsertAsync(Review review);
        Task UpdateAsync(Review review);
        Task DeleteAsync(Review review);

        Task<IEnumerable<Review>> FindAllAsync(int orderId);
    }
}
