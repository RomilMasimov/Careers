using System;
using System.Collections.Generic;

namespace Careers.Models
{
    public class Client:Person
    {
        public bool? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<UserSpecialistMessage> UserSpecialistMessages { get; set; }
    }
}
