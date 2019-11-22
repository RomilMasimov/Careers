using System.Collections.Generic;

namespace Careers.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<City> Cities { get; set; }
    }
}
