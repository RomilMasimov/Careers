using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Careers.Models
{
    public class Client
    {
        public int Id { get; set; }
        public bool Test { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
        public IEnumerable<Order> Orders { get; set; }

    }
}
