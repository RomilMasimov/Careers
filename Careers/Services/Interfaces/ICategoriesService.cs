using System.Collections.Generic;
using System.Threading.Tasks;
using Careers.Models;

namespace Careers.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task<Category> InsertCategoryAsync(Category category);
        Task<SubCategory> InsertSubCategoryAsync(SubCategory subCategory);
        Task<Category> UpdateCategoryAsync(Category category);
        Task<SubCategory> UpdateSubCategoryAsync(SubCategory subCategory);
        Task<bool> DeleteCategoryAsync(Category category);
        Task<bool> DeleteSubCategoryAsync(SubCategory subCategory);

        Task<IEnumerable<Category>> GetAllCategories();
        Task<IEnumerable<Category>> GetPopularCategories();
        Task<IEnumerable<SubCategory>> FindAllSubCategories(int categoryId);
    }
}
