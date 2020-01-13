using System.Collections.Generic;

namespace Careers.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string DescriptionAZ { get; set; }
        public string DescriptionRU { get; set; }

        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
       // public IEnumerable<ServiceReview> ServiceReviews { get; set; }
        public IEnumerable<SpecialistService> SpecialistServices { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Question> Questions { get; set; }

    }
}