using System.Collections.Generic;

namespace Careers.Models
{
    public class SubCategory
    {
        public int  Id { get; set; }
        public string DescriptionAZ { get; set; }
        public string DescriptionRU { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Service> Services { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        //public IEnumerable<DefaultQuestion> DefaultQuestions { get; set; }
    }
}