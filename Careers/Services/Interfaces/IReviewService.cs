using System.Collections.Generic;
using System.Threading.Tasks;
using Careers.Models;

namespace Careers.Services.Interfaces
{
  public  interface IReviewService
    {
        Task<Review> InsertAsync(Review review);
        Task<Review> UpdateAsync(Review review);
        Task<bool> DeleteAsync(Review review);
        Task WriteCommentAsync(ReviewComment reviewComment, Message message);
        Task<IEnumerable<Message>> GetCommentsAsync(int reviewOrCommentReviewId);
        Task<IEnumerable<Review>> GetLastReviewsAsync(int count);
        Task<IEnumerable<Review>> GetBestLastReviewsAsync(int count);
        Task<IEnumerable<Review>> FindAllAsync(int orderId);
    }
}
