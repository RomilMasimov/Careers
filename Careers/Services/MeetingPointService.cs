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
    }
}
