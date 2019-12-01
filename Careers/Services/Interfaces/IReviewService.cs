using System.Collections.Generic;
using System.Threading.Tasks;
using Careers.Models;

namespace Careers.Services.Interfaces
{
    interface IReviewService
    {
        Task<Review> InsertAsync(Review review);
        Task<Review> UpdateAsync(Review review);
        Task<bool> DeleteAsync(Review review);

        Task<IEnumerable<Review>> FindAllAsync(int orderId);
    }
}
