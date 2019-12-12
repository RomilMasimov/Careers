﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Careers.EF;
using Careers.Models;
using Careers.Repositories;
using Careers.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Careers.Services
{
    public class SpecialistService : ISpecialistService
    {
        private readonly CareersDbContext context;
        private readonly MediaRepository mediaRepository;
        private readonly string mediaPath = Environment.CurrentDirectory + @"/Media";

        public SpecialistService(CareersDbContext context, MediaRepository mediaRepository)
        {
            this.context = context;
            this.mediaRepository = mediaRepository;
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
            return await context.Specialists.FirstOrDefaultAsync(x => x.AppUserId == userId);
        }

        public Task<IEnumerable<Specialist>> FindAllAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdatePasport(int specialistId, Stream image)
        {
            var newPasportPath = await mediaRepository.AddAsync(mediaPath, image);
            if (newPasportPath == string.Empty) return false;

            var specialist = await context.Specialists.FindAsync(specialistId);
            if(!string.IsNullOrWhiteSpace(specialist.PassportPath)) 
                mediaRepository.Delete(mediaPath, specialist.PassportPath);
            specialist.PassportPath = newPasportPath;
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateImage(int specialistId, Stream image)
        {
            var newImagePath = await mediaRepository.AddAsync(mediaPath, image);
            if (newImagePath == string.Empty) return false;

            var specialist = await context.Specialists.FindAsync(specialistId);
            if (!string.IsNullOrWhiteSpace(specialist.ImageUrl))
                mediaRepository.Delete(mediaPath, specialist.ImageUrl);
            specialist.ImageUrl = newImagePath;
            context.Specialists.Update(specialist);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteWork(int workId)
        {
            var specialistWork = await context.SpecialistWorks.FindAsync(workId);
            mediaRepository.Delete(mediaPath, specialistWork.ImagePath);
            context.Remove(specialistWork);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<SpecialistWork> AddWork(int specialistId, Stream file, string description)
        {
            var imagePath = await mediaRepository.AddAsync(mediaPath, file);
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
            var lastEducation = await context.Educations.FindAsync(education.Id);
            if (lastEducation == null) return null;
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

        public async Task<Specialist> UpdateAbotAsync(int specialistId, string about)
        {
            var specialist = await context.Specialists.FindAsync(specialistId);
            specialist.About = about;
            var result = context.Specialists.Update(specialist);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
