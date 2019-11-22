using System.Collections.Generic;

namespace Careers.Models
{
    public class MeetingPointType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<MeetingPoint> MeetingPoints { get; set; }
    }
}