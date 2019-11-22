using System.Collections.Generic;

namespace Careers.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<ServicePrice> ServicePrices { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}