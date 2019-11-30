using System;

namespace Careers.Models
{
    public class Experience
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }

        public Specialist Specialist { get; set; }
        public int SpecialistId { get; set; }
    }
}