namespace Careers.Models
{
    public class SpecialistAnswer
    {
        public int Id { get; set; }
        public int SpecialistId { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
        public Specialist Specialist { get; set; }
    }
}
