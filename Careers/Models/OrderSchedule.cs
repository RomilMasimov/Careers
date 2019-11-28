using System;

namespace Careers.Models
{
    public class OrderSchedule
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
     
        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
