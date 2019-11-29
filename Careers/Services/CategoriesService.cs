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
        public Task DeleteCategoryAsync(Category category)
        {
            context.Categories.Remove(category);
            return context.SaveChangesAsync();
        }

        public Task DeleteSubCategoryAsync(SubCategory subCategory)
        {
            context.SubCategories.Remove(subCategory);
            return context.SaveChangesAsync();
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

        public async Task InsertCategoryAsync(Category category)
        {
            category.Id = 0;
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
        }

        public async Task InsertSubCategoryAsync(SubCategory subCategory)
        {
            subCategory.Id = 0;
            await context.SubCategories.AddAsync(subCategory);
            await context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync();
        }

        public async Task UpdateSubCategoryAsync(SubCategory subCategory)
        {
            await context.SubCategories.Update(subCategory);
            await context.SaveChangesAsync();
        }
    }
}
