using System;
using System.Collections.Generic;

namespace Careers.Models
{
    public class Specialist:Person
    {
        public string Fathername { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string About { get; set; }
        public string PassportPath { get; set; }
        public int Balance { get; set; }


        //settings
        public bool TakeOrders { get; set; }
        public bool ReceiveMessages { get; set; }
        public bool ReceiveNotifications { get; set; }
        //end


        //relationships
        public City City { get; set; }
        public int CityId { get; set; }
        public IEnumerable<LanguageSpecialist> LanguageSpecialists { get; set; }
        public IEnumerable<SpecialistWork> SpecialistWorks { get; set; }
        public IEnumerable<Education> Educations { get; set; }
        public IEnumerable<Experience> Experiences { get; set; }
        public IEnumerable<WhereCanMeetSpecialist> WhereCanMeetList { get; set; }
        public IEnumerable<WhereCanGoSpecialist> WhereCanGoList { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<SpecialistService> SpecialistServices { get; set; }
        public IEnumerable<SpecialistAnswer> SpecialistAnswers { get; set; }
        public IEnumerable<UserSpecialistMessage> UserSpecialistMessages { get; set; }
        public IEnumerable<OrderResponce> OrderResponces { get; set; }

    }
}
