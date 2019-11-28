using System.Collections.Generic;

namespace Careers.Models
{
    public class Client
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
