using System.Collections.Generic;
using System.Threading.Tasks;
using Careers.Models;

namespace Careers.Services.Interfaces
{
    interface IMeetingPointService
    {
        //meeting point type
        Task<MeetingPointType> InsertMeetingPointTypeAsync(string name);
        Task<MeetingPointType> UpdateMeetingPointTypeAsync(MeetingPointType meetingPointType);
        Task<bool> DeleteMeetingPointTypeAsync(MeetingPointType meetingPointType);
        Task<IEnumerable<MeetingPointType>> FindAllMeetingPointTypesAsync();

        //meeting point
        Task<MeetingPoint> InsertMeetingPointAsync(MeetingPoint meetingPoint);
        Task<MeetingPoint> UpdateMeetingPointAsync(MeetingPoint meetingPoint);
        Task<bool> DeleteMeetingPointAsync(MeetingPoint meetingPoint);
        Task<MeetingPoint> FindAsync(int id);
        Task<MeetingPoint> FindAsync(string description);
        Task<IEnumerable<MeetingPoint>> FindAllAsync();
    }
}
