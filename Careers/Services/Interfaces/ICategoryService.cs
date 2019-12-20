using System.Collections.Generic;
using System.Threading.Tasks;
using Careers.Models;

namespace Careers.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> GetCategoryAsync(int id);
        Task<Category> GetCategoryAsync(string name);
        Task<Category> GetCategoryAndSubCategoriesAsync(int id);
        Task<Category> GetCategoryAndSubCategoriesAsync(string name);
        
        Task<Category> InsertAsync(Category category);
        Task<SubCategory> InsertAsync(SubCategory subCategory);
        Task<Category> UpdateAsync(Category category);
        Task<SubCategory> UpdateAsync(SubCategory subCategory);
        Task<bool> DeleteAsync(Category category);
        Task<bool> DeleteAsync(SubCategory subCategory);

        Task<IEnumerable<Category>> GetAllCategories(bool includeSubcategories = false);
        Task<IEnumerable<Category>> GetPopularCategories();
        Task<IEnumerable<SubCategory>> GetAllSubCategories(int categoryId);

        Task<IEnumerable<Service>> GetServicesAsync(int subCategoryId);
    }
}
