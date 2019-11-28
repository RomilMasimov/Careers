using System.Threading.Tasks;
using Careers.Models;

namespace Careers.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task InsertAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category categoryt);

        Task<Category> FindAsync(int id);
    }
}
