using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Careers.Models;
using Careers.Services.Interfaces;

namespace Careers.Services
{
    public class MeetingPointsService:IMeetingPointsService
    {
        public Task<MeetingPointType> InsertMeetingPointTypeAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<MeetingPointType> EditMeetingPointTypeAsync(MeetingPointType meetingPointType)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteMeetingPointTypeAsync(MeetingPointType meetingPointType)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MeetingPointType>> FindAllMeetingPointTypesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MeetingPoint> InsertMeetingPointAsync(MeetingPoint meetingPoint)
        {
            throw new NotImplementedException();
        }

        public Task<MeetingPoint> UpdateMeetingPointAsync(MeetingPoint meetingPoint)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteMeetingPointAsync(MeetingPoint meetingPoint)
        {
            throw new NotImplementedException();
        }

        public Task<MeetingPoint> FindAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MeetingPoint> FindAsync(string description)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MeetingPoint>> FindAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
