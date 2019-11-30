namespace Careers.Models
{
    public class LanguageSpecialist
    {
        public int Id { get; set; }
        public Specialist Specialist { get; set; }
        public int SpecialistId { get; set; }
        public MyLanguage Language { get; set; }
        public int LanguageId { get; set; }
    }
}