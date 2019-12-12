﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Careers.EF;
using Careers.Models;
using Careers.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Careers.Services
{
    public class SpecialistService : ISpecialistService
    {
        private readonly CareersDbContext _context;

        public SpecialistService(CareersDbContext context)
        {
            _context = context;
        }

        public async Task<Specialist> InsertAsync(Specialist specialist)
        {
            specialist.Id = 0;
            specialist.LastVisit = DateTime.Now;

            var spec = await _context.Specialists.AddAsync(specialist);
            await _context.SaveChangesAsync();
            return spec.Entity;
        }

        public Task<Specialist> UpdateAsync(Specialist specialist)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Specialist specialist)
        {
            throw new NotImplementedException();
        }

        public async Task<Specialist> FindAsync(int id)
        {
            return await _context.Specialists.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Specialist> FindAsync(string userId)
        {
            return await _context.Specialists.FirstOrDefaultAsync(x => x.AppUserId == userId);
        }

        public Task<IEnumerable<Specialist>> FindAllAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
