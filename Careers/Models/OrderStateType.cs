using System.Collections.Generic;

namespace Careers.Models
{
    public class OrderStateType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}