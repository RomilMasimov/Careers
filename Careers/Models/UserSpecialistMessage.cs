namespace Careers.Models
{
    public class UserSpecialistMessage
    {
        public int Id { get; set; }
        public string LogFilePath { get; set; }

        public bool? SpecialistRead { get; set; }
        public bool? ClientRead { get; set; }
            
        public Specialist Specialist { get; set; }
        public int SpecialistId { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
