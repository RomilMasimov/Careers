using System.Collections.Generic;

namespace Careers.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public IEnumerable<SubCategory> SubCategories { get; set; }
        public IEnumerable<Question> Questions { get; set; }
    }
}
