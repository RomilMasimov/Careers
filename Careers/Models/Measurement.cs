using System.Collections.Generic;

namespace Careers.Models
{
    public class Measurement
    {
        public int Id { get; set; }
        public string TextAZ { get; set; }
        public string TextRU { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}
