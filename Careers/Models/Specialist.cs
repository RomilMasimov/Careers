using System;
using System.Collections.Generic;

namespace Careers.Models
{
    public class Specialist
    {
        public int Id { get; set; }

        //public string Speciality { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Fathername { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImageUrl { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string About { get; set; }
        public string PassportPath { get; set; }
        public IEnumerable<Education> Educations { get; set; }
        public IEnumerable<Experience> Experiences { get; set; }
        //public IEnumerable<string> DocsCertificates { get; set; }
        //public IEnumerable<ServicePrice> ServicesPrices { get; set; }
        //public IEnumerable<ImagePath> MyWorksImagesUrl { get; set; }

        public City City { get; set; }
        public int CityId { get; set; }
        public IEnumerable<WhereCanMeetSpecialist> WhereCanMeetList { get; set; }
        public IEnumerable<WhereCanGoSpecialist> WhereCanGoList { get; set; }
        public IEnumerable<OrderSpecialist> OrderSpecialists { get; set; }
        public IEnumerable<SpecialistService> SpecialistServices { get; set; }
        public IEnumerable<SpecialistAnswer> SpecialistAnswers { get; set; }
        public IEnumerable<UserSpecialistMessages> UserSpecialistMessageses { get; set; }

    }
}
