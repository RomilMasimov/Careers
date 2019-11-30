namespace Careers.Models
{
    public class SpecialistWork
    {
        public int Id { get; set; }
        public Specialist Specialist { get; set; }
        public int SpecialistId { get; set; }
        public string ImagePath { get; set; }
    }
}