using System.Collections.Generic;

namespace Careers.Models
{
    public class Order
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        
        public Client Client { get; set; }
        public int ClientId { get; set; }
    
        public IEnumerable<OrderMeetingPoint> OrderMeetingPoints { get; set; }
        public IEnumerable<OrderReview> OrderReviews { get; set; }
        public IEnumerable<AnswerOrder> AnswerOrders { get; set; }
        public IEnumerable<OrderSchedule> OrderSchedules { get; set; }
        public IEnumerable<OrderSpecialist> OrderSpecialists { get; set; }


    }
}
