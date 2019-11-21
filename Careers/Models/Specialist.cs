using System.Collections;
using System.Collections.Generic;

namespace Careers.Models
{
    public class Specialist
    {
        public string Speciality { get; set; }
        public IEnumerable<MeetingPoint> WhereAccept { get; set; }
        public IEnumerable<MeetingPoint> WhereCanGo { get; set; }
        public string About { get; set; }
        public IEnumerable<Education> Educations { get; set; }
        public IEnumerable<Experience> Experiences { get; set; }
        public string AdditionalInfo { get; set; }
        public IEnumerable<string> DocsCertificates { get; set; }
        public IEnumerable<ServicePrice> ServicesPrices { get; set; }
        public IEnumerable<string> MyWorkImages { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }

   
}
