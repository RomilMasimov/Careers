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
    public class CategoriesService : ICategoriesService
    {
        private readonly CareersDbContext context;

        public CategoriesService(CareersDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> DeleteCategoryAsync(Category category)
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSubCategoryAsync(SubCategory subCategory)
        {
            context.SubCategories.Remove(subCategory);
            await context.SaveChangesAsync();
            return true;

        }

        public async Task<IEnumerable<SubCategory>> FindAllSubCategories(int categoryId)
        {
            return await context.SubCategories.Where(m => m.CategoryId == categoryId).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await context.Categories
                .Include(m => m.SubCategories)
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetPopularCategories()
        {
            var avgServicesCount = await context.Categories.AverageAsync(m => m.SubCategories.Count());
            return await context.Categories
                .Where(m => m.SubCategories.Count() > (int)Math.Floor(avgServicesCount))
                .Include(m => m.SubCategories)
                .ToListAsync();
        }

        public async Task<Category> InsertCategoryAsync(Category category)
        {
            category.Id = 0;
            var res = await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<SubCategory> InsertSubCategoryAsync(SubCategory subCategory)
        {
            subCategory.Id = 0;
            var res = await context.SubCategories.AddAsync(subCategory);
            await context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            var res = context.Categories.Update(category);
            await context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<SubCategory> UpdateSubCategoryAsync(SubCategory subCategory)
        {
            var res = context.SubCategories.Update(subCategory);
            await context.SaveChangesAsync();
            return res.Entity;
        }
    }
}
