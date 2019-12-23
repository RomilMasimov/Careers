using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task<Specialist> FindAsync(int id)
        {
            return await context.Specialists.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Specialist> FindByUserAsync(string userId)
        {
            return await context.Specialists.Include(m => m.AppUser).FirstOrDefaultAsync(x => x.AppUserId == userId);
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

        public async Task<bool> UpdateWhereCanGo(Specialist specialist, int[] pointsId)
        {
            specialist.WhereCanGoList.ToList().RemoveAll(x => !pointsId.Contains(x.Id));
            var oldPoints = context.WhereCanGoSpecialists.Where(x => x.SpecialistId == specialist.Id).ToList();
            var newPoints = oldPoints.Where(m => !pointsId.Contains(m.Id));
            specialist.WhereCanGoList.ToList().AddRange(newPoints);
            //context.UpdateManyToMany(
            //        context.WhereCanGoSpeciaists.Where(x => x.SpecialistId == specialistId),
            //        pointsId.Select(x => new WhereCanGoSpecialist { SpecialistId = specialistId, WhereCanGoId = x }),
            //        x => x.WhereCanGoId);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateWhereCanMeet(int specialistId, int[] pointsId)
        {
            context.UpdateManyToMany(
                    context.WhereCanGoSpecialists.Where(x => x.SpecialistId == specialistId),
                    pointsId.Select(x => new WhereCanGoSpecialist { SpecialistId = specialistId, WhereCanGoId = x }),
                    x => x.WhereCanGoId);
            return await context.SaveChangesAsync() > 0;
        }
    }
}
