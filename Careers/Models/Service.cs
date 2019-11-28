    using System.Collections.Generic;

namespace Careers.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public IEnumerable<SpecialistService> SpecialistServices { get; set; }


    }
}