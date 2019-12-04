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
        public DateTime? DateOfBirth { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<UserSpecialistMessage> UserSpecialistMessages { get; set; }
    }
}
