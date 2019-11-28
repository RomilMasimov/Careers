using System.Collections.Generic;

namespace Careers.Models
{
    public class Specialist
    {
        public int Id { get; set; }

        //public string Speciality { get; set; }

        public string About { get; set; }
        public string AdditionalInfo { get; set; }
        //public IEnumerable<string> DocsCertificates { get; set; }
        //public IEnumerable<ServicePrice> ServicesPrices { get; set; }
        //public IEnumerable<string> MyWorkImagesUrl { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public IEnumerable<WhereCanMeetSpecialist> WhereCanMeetList { get; set; }
        public IEnumerable<WhereCanGoSpecialist> WhereCanGoList { get; set; }
        public IEnumerable<OrderSpecialist> OrderSpecialists { get; set; }
        public IEnumerable<SpecialistService> SpecialistServices { get; set; }
        public IEnumerable<SpecialistAnswer> SpecialistAnswers { get; set; }

    }
}
