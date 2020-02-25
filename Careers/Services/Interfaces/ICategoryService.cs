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
        Task<Service> InsertAsync(Service service);
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
        Task<Models.SpecialistService> InsertSpecialistServiceAsync(Models.SpecialistService specialistService);
        Task<Models.SpecialistService> UpdateSpecialistServiceAsync(Models.SpecialistService specialistService);
        Task<bool> RemoveFromSpecialistAsync(int specialistId, int id);
        Task<IEnumerable<Measurement>> FindAllMeasurements();
        Task<bool> AddMeasurementAsync(Measurement measurement);
        Task<SubCategory> GetSubCategoryAsync(int subCategoryId);
    }
}
