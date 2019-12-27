using System.Collections.Generic;

namespace Careers.Models
{
    public class SpecialistFilter
    {
        public List<int> CityIds { get; set; }
        public List<int> SubCategoryIds { get; set; }
        public List<int> ServiceIds { get; set; }
        public List<int> LanguageIds { get; set; }
        public int Experience{ get; set; }
        public int Rating { get; set; }
        public int Page { get; set; }


        public SpecialistFilter()
        {
            Page = 1;
            Experience = -1;
            CityIds = new List<int>();
            ServiceIds = new List<int>();
            LanguageIds = new List<int>();
            SubCategoryIds=new List<int>();
        }
    }
}
