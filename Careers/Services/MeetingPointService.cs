using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Careers.EF;
using Careers.Models;
using Careers.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Careers.Services
{
    public class MeetingPointService : IMeetingPointService
    {
        private readonly CareersDbContext context;

        public MeetingPointService(CareersDbContext context)
        {
            this.context = context;
        }

        public async Task<MeetingPoint> InsertMeetingPointAsync(MeetingPoint meetingPoint)
        {
            meetingPoint.Id = 0;
            var result = await context.MeetingPoints.AddAsync(meetingPoint);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<MeetingPoint> UpdateMeetingPointAsync(MeetingPoint meetingPoint)
        {
            context.MeetingPoints.Update(meetingPoint);
            await context.SaveChangesAsync();
            return meetingPoint;
        }

        public async Task<bool> DeleteMeetingPointAsync(MeetingPoint meetingPoint)
        {
            context.MeetingPoints.Remove(meetingPoint);

            return await context.SaveChangesAsync() > 0;
        }

        public async Task<MeetingPoint> FindAsync(int id)
        {
            return await context.MeetingPoints.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<MeetingPoint> FindAsync(string description)
        {
            return await context.MeetingPoints.FirstOrDefaultAsync(x => x.Description == description);
        }

        public async Task<IEnumerable<MeetingPoint>> GetAllAsync()
        {
            return await context.MeetingPoints.ToListAsync();
        }

        public async Task<IEnumerable<MeetingPoint>> GetAllByCityAsync(int cityId)
        {
            return await context.MeetingPoints.Where(m => m.CityId == cityId).ToListAsync();
        }

        public async Task<IEnumerable<MeetingPoint>> GetAllByTextAsync(string text)
        {
            return await context.MeetingPoints.Where(m => m.Description.Contains(text)).ToListAsync();
        }

        public async Task<IEnumerable<MeetingPoint>> FindAllWhereCanMeetBySpecialistAsync(int specialistId)
        {
            return await context.WhereCanMeetSpecialists
                           .Where(m => m.SpecialistId == specialistId)
                           .Include(m => m.WhereCanMeet)
                           .Select(m => m.WhereCanMeet)
                           .ToListAsync();
        }

        public async Task<IEnumerable<MeetingPoint>> FindAllWhereCanGoBySpecialistAsync(int specialistId)
        {
            return await context.WhereCanGoSpecialists
                                       .Where(m => m.SpecialistId == specialistId)
                                       .Include(m => m.WhereCanGo)
                                       .Select(m => m.WhereCanGo)
                                       .ToListAsync();
        }
    }
}
