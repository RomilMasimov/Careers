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
        Task<IEnumerable<SubCategory>> GetAllSubCategoriesByAzTextAsync(string text);
        Task<IEnumerable<SubCategory>> GetAllSubCategoriesByRuTextAsync(string text);

        Task<Service> FindServiceAsync(int serviceId);
        Task<Service> FindServiceAsync(string name);
        Task<IEnumerable<Service>> GetServicesAsync(int subCategoryId);
        Task<IEnumerable<Service>> GetServicesBySubCategoryArrAsync(IEnumerable<int> subCategoryIds);
        Task<IEnumerable<Service>> GetAllServicesAsync();
        Task<IEnumerable<Service>> GetAllServicesByAzTextAsync(string text);
        Task<IEnumerable<Service>> GetAllServicesByRuTextAsync(string text);
        Task<Careers.Models.SpecialistService> InsertSpecialistServiceAsync(Careers.Models.SpecialistService specialistService);
        Task<Careers.Models.SpecialistService> UpdateSpecialistServiceAsync(Careers.Models.SpecialistService specialistService);
        Task<IEnumerable<Measurement>> FindAllMeasurements();
    }
}
