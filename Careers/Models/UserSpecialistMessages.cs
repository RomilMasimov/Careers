namespace Careers.Models
{
    public class UserSpecialistMessages
    {
        public int Id { get; set; }
        public Specialist Specialist { get; set; }
        public int SpecialistId { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public string LogFilePath { get; set; }
    }
}
