namespace Careers.Models
{
    public class OrderMeetingPoint
    {
        public int  Id { get; set; }

        public Order Order { get; set; }
        public int OrderId { get; set; }
        public MeetingPoint MeetingPoint { get; set; }
        public int MeetingPointId { get; set; }
    }
}