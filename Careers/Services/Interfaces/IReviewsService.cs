using System.Collections.Generic;
using System.Threading.Tasks;
using Careers.Models;

namespace Careers.Services.Interfaces
{
    interface IReviewsService
    {
        Task InsertAsync(Review review);
        Task UpdateAsync(Review review);
        Task DeleteAsync(Review review);

        Task<IEnumerable<Review>> FindAllAsync(int orderId);
    }
}
