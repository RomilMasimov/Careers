using System;
using System.Collections.Generic;
using Careers.Models.Identity;

namespace Careers.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ImageUrl { get; set; }
        public bool? Gender { get; set; }
        public bool SmsNotifications { get; set; }
        public bool EmailNotifications { get; set; }
        public DateTime LastVisit { get; set; }

        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<UserSpecialistMessage> UserSpecialistMessages { get; set; }
    }
}
