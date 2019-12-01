using System.Collections.Generic;

namespace Careers.Models
{
    public class Order
    {
        public int Id { get; set; }
        public OrderStateTypeEnum State { get; set; }

        public Client Client { get; set; }
        public int ClientId { get; set; }
        public Specialist Specialist { get; set; }
        public int SpecialistId { get; set; }
        public IEnumerable<OrderMeetingPoint> OrderMeetingPoints { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public IEnumerable<AnswerOrder> AnswerOrders { get; set; }
        public IEnumerable<OrderSchedule> OrderSchedules { get; set; }
       // public IEnumerable<OrderSpecialist> OrderSpecialists { get; set; }


    }
}
