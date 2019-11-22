namespace Careers.Models
{
    public class WhereCanMeetSpecialist
    {
        public int Id { get; set; }

        public MeetingPoint WhereCanMeet { get; set; }
        public int WhereCanMeetId { get; set; }
        public Specialist Specialist { get; set; }
        public int SpecialistId { get; set; }
    }
}
