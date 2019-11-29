using System.Collections.Generic;
using System.Threading.Tasks;
using Careers.Models;

namespace Careers.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task InsertCategoryAsync(Category category);
        Task InsertSubCategoryAsync(SubCategory subCategory);
        Task UpdateCategoryAsync(Category category);
        Task UpdateSubCategoryAsync(SubCategory subCategory);
        Task DeleteCategoryAsync(Category category);
        Task DeleteSubCategoryAsync(SubCategory subCategory);

        Task<IEnumerable<Category>> GetAllCategories();
        Task<IEnumerable<Category>> GetPopularCategories();
        Task<IEnumerable<SubCategory>> FindAllSubCategories(int categoryId);
    }
}
