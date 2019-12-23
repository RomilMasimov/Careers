using System.Collections.Generic;
using System.Threading.Tasks;
using Careers.Models;

namespace Careers.Services.Interfaces
{
   public interface IMeetingPointService
    {
        //meeting point
        Task<MeetingPoint> InsertMeetingPointAsync(MeetingPoint meetingPoint);
        Task<MeetingPoint> UpdateMeetingPointAsync(MeetingPoint meetingPoint);
        Task<bool> DeleteMeetingPointAsync(MeetingPoint meetingPoint);
        Task<MeetingPoint> FindAsync(int id);
        Task<MeetingPoint> FindAsync(string description);
        Task<IEnumerable<MeetingPoint>> GetAllAsync();
        Task<IEnumerable<MeetingPoint>> FindAllWhereCanMeetBySpecialistAsync(int specialistId);
        Task<IEnumerable<MeetingPoint>> FindAllWhereCanGoBySpecialistAsync(int specialistId);
    }
}
