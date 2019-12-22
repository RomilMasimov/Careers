using Careers.Models.Enums;
using System.Collections.Generic;

namespace Careers.Models
{
    public class MeetingPoint
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public MeetingPointTypeEnum MeetingPointType { get; set; }
        public IEnumerable<WhereCanMeetSpecialist> WhereCanMeetList { get; set; }
        public IEnumerable<WhereCanGoSpecialist> WhereCanGoList { get; set; }
        public IEnumerable<OrderMeetingPoint> OrderMeetingPoints { get; set; }

    }
}