using Careers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Services.CategoriesService
{
    public interface ICategoriesService
    {
        Task InsertAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category categoryt);

        Task<Category> FindAsync(int id);
    }
}
