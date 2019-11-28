using System.Collections.Generic;

namespace Careers.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string DescriptionAZ { get; set; }
        public string DescriptionRU { get; set; }

        public IEnumerable<SubCategory> SubCategories { get; set; }
    }
}
