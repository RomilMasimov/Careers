namespace Careers.Models
{
    public class UserSpecialistMessage
    {
        public int Id { get; set; }
        //path to json file
        public string LogFilePath { get; set; }

        public Specialist Specialist { get; set; }
        public int SpecialistId { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
    }
}
