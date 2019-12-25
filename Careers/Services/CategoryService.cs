using Careers.EF;
using Careers.Models;
using Careers.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CareersDbContext context;

        public CategoryService(CareersDbContext context)
        {
            this.context = context;
        }

        public async Task<Category> GetCategoryAndSubCategoriesAsync(int id)
        {
            var res = await context.Categories
                .Include(x=>x.SubCategories)
                .ThenInclude(x=>x.Services)
                .FirstOrDefaultAsync(x=>x.Id==id);
            return res;
        }

        public async Task<Category> GetCategoryAndSubCategoriesAsync(string name)
        {
            var res = await context.Categories
                .Include(x => x.SubCategories)
                .ThenInclude(x => x.Services)
                .FirstOrDefaultAsync(x => x.DescriptionRU.ToLower() == name.ToLower());
            return res;
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            var res = await context.Categories
                .FirstOrDefaultAsync(x => x.Id == id);
            return res;
        }

        public async Task<Category> GetCategoryAsync(string name)
        {
            var res = await context.Categories
                .FirstOrDefaultAsync(x => x.DescriptionRU.ToLower() == name.ToLower());
            return res;
        }

        public async Task<bool> DeleteAsync(Category category)
        {
            context.Categories.Remove(category);

            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(SubCategory subCategory)
        {
            context.SubCategories.Remove(subCategory);

            return await context.SaveChangesAsync() > 0;

        }

        public async Task<IEnumerable<SubCategory>> GetAllSubCategories(int categoryId)
        {
            return await context.SubCategories.Where(m => m.CategoryId == categoryId).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategories(bool includeSubcategories = false)
        {
            if (includeSubcategories)
            {
                return await context.Categories
                    .Include(m => m.SubCategories)
                    .ToListAsync();
            }

            return await context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetPopularCategories()
        {
            var avgServicesCount = await context.Categories.AverageAsync(m => m.SubCategories.Count());
            return await context.Categories
                .Where(m => m.SubCategories.Count() > (int)Math.Floor(avgServicesCount))
                .Include(m => m.SubCategories)
                .ToListAsync();
        }

        public async Task<Category> InsertAsync(Category category)
        {
            category.Id = 0;
            var res = await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<SubCategory> InsertAsync(SubCategory subCategory)
        {
            subCategory.Id = 0;
            var res = await context.SubCategories.AddAsync(subCategory);
            await context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            var res = context.Categories.Update(category);
            await context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<SubCategory> UpdateAsync(SubCategory subCategory)
        {
            var res = context.SubCategories.Update(subCategory);
            await context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<IEnumerable<Service>> GetServicesAsync(int subCategoryId)
        {
            return await context.Services.Where(x => x.SubCategoryId == subCategoryId).ToListAsync();
        }

        public Task<Service> FindServiceAsync(int serviceId)
        {
            return context.Services.FindAsync(serviceId).AsTask();
        }

        public async Task<IEnumerable<Measurement>> FindAllMeasurements()
        {
            return await context.Measurements.ToListAsync();
        }

        public async Task<Careers.Models.SpecialistService> InsertSpecialistServiceAsync(Careers.Models.SpecialistService specialistService)
        {
            specialistService.Id = 0;
            var res = await context.SpecialistServices.AddAsync(specialistService);
            await context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Careers.Models.SpecialistService> UpdateSpecialistServiceAsync(Careers.Models.SpecialistService specialistService)
        {
            var res = context.SpecialistServices.Update(specialistService);
            await context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<IEnumerable<Service>> GetAllServicesAsync()
        {
            return await context.Services.ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetServicesBySubCategoryArrAsync(IEnumerable<int> subCategoryIds)
        {
            return await context.Services.Where(x => subCategoryIds.Contains(x.SubCategoryId)).ToListAsync();
        }
    }
}
