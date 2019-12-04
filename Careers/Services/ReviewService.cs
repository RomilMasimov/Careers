using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Careers.EF;
using Careers.Models;
using Careers.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Careers.Services
{
    public class ReviewService : IReviewService
    {

        private readonly CareersDbContext _context;

        public ReviewService(CareersDbContext context)
        {
            _context = context;
        }

        public async Task WriteCommentAsync(ReviewComment reviewComment, Message message)
        {
            if (reviewComment.Id != 0)
            {
                if (reviewComment.LogFilePath != null)
                {
                    await using FileStream fs = new FileStream(reviewComment.LogFilePath, FileMode.Append, FileAccess.Write);
                    await using StreamWriter sw = new StreamWriter(fs);
                    await sw.WriteLineAsync(JsonSerializer.Serialize(message));
                    return;
                }
            }

            {
                var path = Environment.CurrentDirectory + @"\CommentsLog\" + $"{new Guid()}.txt";
                reviewComment.LogFilePath = path;
                await _context.ReviewComments.AddAsync(reviewComment);
                await _context.SaveChangesAsync();

                await using FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
                await using StreamWriter sw = new StreamWriter(fs);
                await sw.WriteLineAsync(JsonSerializer.Serialize(message));
            }
        }

        private async Task<IEnumerable<Message>> commentBodyAsync(ReviewComment reviewComment)
        {
            if (reviewComment == null) return null;

            string jsonStrings;
            await using (FileStream fs = new FileStream(reviewComment.LogFilePath, FileMode.Open, FileAccess.Read))
            {
                using var sw = new StreamReader(fs);
                jsonStrings = await sw.ReadToEndAsync();
            }

            var splitedJsonStrings = jsonStrings.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var messages = splitedJsonStrings.Select(item => JsonSerializer.Deserialize<Message>(item)).ToList();

            if (!messages.Any()) return null;
            return messages;
        }

        public async Task<IEnumerable<Message>> GetCommentsAsync(int reviewOrCommentReviewId)
        {
            var result = await _context.ReviewComments
                    .FirstOrDefaultAsync(x => x.Id == reviewOrCommentReviewId || x.ReviewId== reviewOrCommentReviewId);

            return await commentBodyAsync(result);
        }


        public Task<Review> InsertAsync(Review review)
        {
            throw new NotImplementedException();
        }

        public Task<Review> UpdateAsync(Review review)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Review review)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> FindAllAsync(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
