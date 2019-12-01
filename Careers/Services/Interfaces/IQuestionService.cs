using Careers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<Question> InsertAsync(Question question);
        Task<Question> UpdateAsync(Question question);
        Task<bool> DeleteAsync(Question question);
        Task<IEnumerable<Question>> FindAllAsync(int subCategoryId);
    }
}
