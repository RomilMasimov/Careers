using System.Collections.Generic;

namespace Careers.Models
{
    public class Order
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public Specialist Specialist { get; set; }
        public int? SpecialistId { get; set; }
        public IEnumerable<OrderMeetingPoint> OrderMeetingPoints { get; set; }
        public IEnumerable<OrderReview> OrderReviews { get; set; }
        private IEnumerable<Answer> Details { get; set; }

    }
}
