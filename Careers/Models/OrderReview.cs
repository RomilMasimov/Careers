using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Models
{
    public class OrderReview
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public Review Review { get; set; }
        public int? ReviewId { get; set; }
    }
}
