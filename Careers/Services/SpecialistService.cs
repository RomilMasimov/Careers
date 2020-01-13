using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Careers.EF;
using Careers.Models;
using Careers.Services.Interfaces;
using Careers.Helpers;
using Microsoft.EntityFrameworkCore;
using Careers.Models.Enums;
using Microsoft.AspNetCore.Http;
using BlogWebsite.Extensions;

namespace Careers.Services
{
    public class SpecialistService : ISpecialistService
    {
        private readonly CareersDbContext context;

        public SpecialistService(CareersDbContext context)
        {
            this.context = context;
        }

        public async Task<Specialist> InsertAsync(Specialist specialist)
        {
            specialist.Id = 0;
            specialist.LastVisit = DateTime.Now;

            var spec = await context.Specialists.AddAsync(specialist);
            await context.SaveChangesAsync();
            return spec.Entity;
        }

        public async Task<Specialist> UpdateAsync(Specialist specialist)
        {
            var result = context.Specialists.Update(specialist);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteAsync(Specialist specialist)
        {
            context.Specialists.Remove(specialist);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<List<Specialist>> GetByFilterAsync(SpecialistFilter filter)
        {
            IQueryable<Specialist> query = context.Specialists;


            if (filter.CityIds.Any())
            {
                query = query
                    .Where(x => filter.CityIds
                    .Contains(x.CityId));
            }

            if (filter.LanguageIds.Any())
            {
                query = query.Include(x => x.LanguageSpecialists)
                .Where(x => x.LanguageSpecialists
                    .Any(y => filter.LanguageIds
                        .Contains(y.LanguageId)));
            }

            if (filter.Rating > 1)
            {
                query = query.Include(z => z.Orders)
                    .ThenInclude(z => z.Reviews)
                    .Where(x => x.Orders
                        .Any(y => y.Reviews
                        .Any(q => q.Mark >= filter.Rating)));
            }


            if (filter.Experience > -1)
            {
                switch (filter.Experience)
                {
                    case 1:
                        query = query.Include(x => x.Experiences).Where(x =>
                            x.Experiences.Select(y => (y.EndDate - y.EndDate).Value.Days)
                                .Any(q => q >= 0 && q <= filter.Experience * 364)); break;
                    case 2:
                        query = query.Include(x => x.Experiences).Where(x =>
                            x.Experiences.Select(y => (y.EndDate - y.EndDate).Value.Days)
                                .Any(q => q >= 1 * 364 && q <= filter.Experience * 364)); break;
                    case 5:
                        query = query.Include(x => x.Experiences).Where(x =>
                            x.Experiences.Select(y => (y.EndDate - y.EndDate).Value.Days)
                                .Any(q => q >= 2 * 364 && q <= filter.Experience * 364)); break;
                    case 6:
                        query = query.Include(x => x.Experiences).Where(x =>
                            x.Experiences.Select(y => (y.EndDate - y.EndDate).Value.Days)
                                .Any(q => q >= 5 * 364 && q <= 20 * 364)); break;
                }
            }

            if (filter.ServiceIds.Any())
            {
                query = query.Include(x => x.SpecialistServices)
                    .ThenInclude(x => x.Service)
                    .Where(x => x.SpecialistServices
                        .Any(d => filter.ServiceIds.Contains(d.ServiceId)));
            }

            if (filter.SubCategoryIds.Any())
            {
                query = query
                    .Include(x => x.SpecialistSubCategories)
                    .Where(x => x.SpecialistSubCategories
                        .Any(s => filter.SubCategoryIds.Contains(s.SubCategoryId)));
            }

            if (filter.Page == 1) return await query.Take(20).ToListAsync();
            var result = await query.ToListAsync();

            return result.Skip((filter.Page - 1) * 20).Take(20).ToList();
        }

        public async Task<Specialist> FindAsync(int id)
        {
            return await context.Specialists.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Specialist> FindDetailedAsync(int id)
        {
            return await context.Specialists
                .Include(x => x.AppUser)
                .Include(x => x.City)
                .Include(x => x.SpecialistWorks)
                .Include(x => x.LanguageSpecialists)
                .ThenInclude(x => x.Language)
                .Include(x => x.Educations)
                .Include(x => x.Experiences)
                .Include(x => x.Orders)
                .ThenInclude(x => x.Client)
                .Include(x => x.Orders)
                .ThenInclude(x => x.Reviews)
                .ThenInclude(x => x.ImagePathes)
                .Include(x => x.SpecialistSubCategories)
                .ThenInclude(x => x.SubCategory)
                .Include(x => x.SpecialistServices)
                .ThenInclude(x => x.Service)
                .Include(x => x.SpecialistServices)
                .ThenInclude(x => x.Measure)
                .Include(x => x.WhereCanGoList)
                .ThenInclude(x => x.WhereCanGo)
                .Include(x => x.WhereCanMeetList)
                .ThenInclude(x => x.WhereCanMeet)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Specialist> FindByUserAsync(string userId)
        {
            return await context.Specialists
                            .Include(m => m.AppUser)
                            .Include(m => m.City)
                            .Include(m => m.WhereCanGoList)
                            .ThenInclude(m => m.WhereCanGo)
                            .Include(m => m.WhereCanMeetList)
                            .ThenInclude(m => m.WhereCanMeet)
                            .Include(m => m.SpecialistSubCategories)
                            .ThenInclude(m => m.SubCategory)
                            .ThenInclude(m => m.Category)
                            .Include(m => m.SpecialistServices)
                            .ThenInclude(m => m.Measure)
                            .FirstOrDefaultAsync(x => x.AppUserId == userId);
        }

        public Task<IEnumerable<Specialist>> FindAllAsync(Order order)
        {
            throw new NotImplementedException();
        }



        public async Task<bool> UpdatePasport(int specialistId, IFormFile image)
        {
            var newPasportPath = await FileUploadHelper.UploadAsync(image, ImageOwnerEnum.Specialist);
            if (newPasportPath == string.Empty) return false;

            var specialist = await context.Specialists.FindAsync(specialistId);
            if (!string.IsNullOrWhiteSpace(specialist.PassportPath))
                FileUploadHelper.Delete(specialist.PassportPath);
            specialist.PassportPath = newPasportPath;
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateImage(int specialistId, IFormFile image)
        {
            var newImagePath = await FileUploadHelper.UploadAsync(image, ImageOwnerEnum.Specialist);
            if (newImagePath == string.Empty) return false;

            var specialist = await context.Specialists.FindAsync(specialistId);
            if (!string.IsNullOrWhiteSpace(specialist.ImageUrl))
                FileUploadHelper.Delete(specialist.ImageUrl);
            specialist.ImageUrl = newImagePath;
            context.Specialists.Update(specialist);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteWork(int workId)
        {
            if (workId <= 0) return false;
            var specialistWork = await context.SpecialistWorks.FindAsync(workId);
            FileUploadHelper.Delete(specialistWork.ImagePath);
            context.SpecialistWorks.Remove(specialistWork);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<SpecialistWork> AddWork(int specialistId, IFormFile file, string description)
        {
            var works = context.SpecialistWorks.Where(m => m.SpecialistId == specialistId);
            if (works.Count() >= 10) return null;

            var imagePath = await FileUploadHelper.UploadAsync(file, ImageOwnerEnum.Specialist);
            if (imagePath == string.Empty) return null;

            var specialistWork = new SpecialistWork { SpecialistId = specialistId, ImagePath = imagePath, Description = description };
            var result = await context.SpecialistWorks.AddAsync(specialistWork);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Education> AddEducation(Education education)
        {
            education.Id = 0;
            if (education.SpecialistId <= 0) return null;
            var result = await context.Educations.AddAsync(education);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Education> UpdateEducation(Education education)
        {
            var result = context.Educations.Update(education);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteEducation(int id)
        {
            var education = await context.Educations.FindAsync(id);
            if (education == null) return false;
            context.Educations.Remove(education);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<Experience> AddExperience(Experience experience)
        {
            experience.Id = 0;
            if (experience.SpecialistId <= 0) return null;
            var result = await context.Experiences.AddAsync(experience);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Experience> UpdateExperience(Experience experience)
        {
            var result = context.Experiences.Update(experience);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteExperience(int id)
        {
            var experience = await context.Experiences.FindAsync(id);
            if (experience == null) return false;
            context.Experiences.Remove(experience);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<Specialist> UpdateAbotAsync(int specialistId, string about)
        {
            var specialist = await context.Specialists.FindAsync(specialistId);
            specialist.About = about;
            var result = context.Specialists.Update(specialist);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteImage(int specialistId)
        {
            var specialist = await context.Specialists.FindAsync(specialistId);
            if (!string.IsNullOrWhiteSpace(specialist.ImageUrl))
                FileUploadHelper.Delete(specialist.ImageUrl);
            specialist.ImageUrl = string.Empty;
            context.Specialists.Update(specialist);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<SpecialistWork>> FindAllWorks(int specialistId)
        {
            return await context.SpecialistWorks.Where(m => m.SpecialistId == specialistId).ToListAsync();
        }

        public Task<SpecialistWork> FindWork(int specialistWorkId)
        {
            return context.SpecialistWorks.FindAsync(specialistWorkId).AsTask();
        }

        public async Task<SpecialistWork> EditWork(int workId, string description)
        {
            var work = await context.SpecialistWorks.FindAsync(workId);
            work.Description = description;
            var result = context.SpecialistWorks.Update(work);
            if (await context.SaveChangesAsync() < 0)
                return null;
            return result.Entity;
        }

        public async Task<IEnumerable<Education>> FindEducationsBySpecialist(int specialistId)
        {
            return await context.Educations.Where(m => m.SpecialistId == specialistId).ToListAsync();
        }

        public Task<Education> FindEducation(int id)
        {
            return context.Educations.FindAsync(id).AsTask();
        }

        public async Task<IEnumerable<Experience>> FindExperiencesBySpecialist(int specialistId)
        {
            return await context.Experiences.Where(m => m.SpecialistId == specialistId).ToListAsync();
        }

        public Task<Experience> FindExperience(int id)
        {
            return context.Experiences.FindAsync(id).AsTask();
        }

        public async Task<bool> UpdateCity(int specialistId, int cityId)
        {
            var specialist = await context.Specialists.FindAsync(specialistId);
            if (specialist.CityId == cityId)
                return false;

            specialist.CityId = cityId;
            await context.SaveChangesAsync();
            await UpdateWhereCanGo(specialist.Id, new int[0]);
            await UpdateWhereCanMeet(specialist.Id, new int[0]);
            return true;
        }

        public async Task<bool> UpdateWhereCanGo(int specialistId, int[] pointsId)
        {
            context.UpdateManyToMany(
                    context.WhereCanGoSpecialists.Where(x => x.SpecialistId == specialistId),
                    pointsId.Select(x => new WhereCanGoSpecialist { SpecialistId = specialistId, WhereCanGoId = x }),
                    x => x.WhereCanGoId);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateWhereCanMeet(int specialistId, int[] pointsId)
        {
            context.UpdateManyToMany(
                    context.WhereCanMeetSpecialists.Where(x => x.SpecialistId == specialistId),
                    pointsId.Select(x => new WhereCanMeetSpecialist { SpecialistId = specialistId, WhereCanMeetId = x }),
                    x => x.WhereCanMeetId);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Specialist>> GetBestByCategoryAsync(int count)
        {
            return await context.Specialists
                .Include(x => x.Orders)
                .ThenInclude(x => x.Service)
                .OrderByDescending(x => x.Orders.Count())
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Service>> FindAllServiceBySpecilalist(int specialistId)
        {
            return await context.SpecialistServices.Where(m => m.SpecialistId == specialistId).Include(m => m.Service).Select(m => m.Service).ToListAsync();
        }

        public Task<bool> UpdateServices(int specialistId, int[] servicesId)
        {
            //context.UpdateManyToMany(
            //    context.SpecialistServices.Where(x => x.SpecialistId == specialistId),
            //    servicesId.Select(x => new Specialist { SpecialistId = specialistId, WhereCanMeetId = x }),
            //    x => x.WhereCanMeetId);
            //return await context.SaveChangesAsync() > 0;
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateSubCategoties(int specialistId, int[] subCategoriesId)
        {
            context.UpdateManyToMany(
                context.SpecialistSubCategories.Where(x => x.SpecialistId == specialistId),
                subCategoriesId.Select(x => new SpecialistSubCategory { SpecialistId = specialistId, SubCategoryId = x }),
                x => x.SubCategoryId);
            return await context.SaveChangesAsync() > 0;
        }
    }
}
