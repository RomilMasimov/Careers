using System;

namespace Careers.Models
{
    public class Education
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string StudyPlaceName { get; set; }
        public string Specialization { get; set; }

        public Specialist Specialist { get; set; }
        public int SpecialistId { get; set; }
    }
}