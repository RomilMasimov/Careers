using Careers.EF;
using Careers.Models;
using Careers.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly CareersDbContext context;

        public QuestionService(CareersDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteAsync(Question question)
        {
            context.Questions.Remove(question);
        
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Question>> FindAllAsync(int subCategoryId)
        {
            return await context.Questions
                .Where(m => m.SubCategoryId == subCategoryId)
                //.Include(m => m.QuestionAnswers)
                .Include(m => m.Answers)
                //.Include(m => m.FromAnswers)
                //.Include(m => m.DefaultQuestions)
                .ToListAsync();
        }

        public async Task<Question> InsertAsync(Question question)
        {
            question.Id = 0;
            var res = await context.Questions.AddAsync(question);
            await context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Question> UpdateAsync(Question question)
        {
            var res = context.Questions.Update(question);
            await context.SaveChangesAsync();
            return res.Entity;
        }
    }
}
