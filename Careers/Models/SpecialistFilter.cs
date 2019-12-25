using System.Collections.Generic;

namespace Careers.Models
{
    public class SpecialistFilter
    {
        public List<int> CityIds { get; set; }
        public List<int> ServiceIds { get; set; }
        public int SubCategoryId { get; set; }
        public List<int> LanguageIds { get; set; }
        public int ExperienceMin { get; set; }
        public int ExperienceMax { get; set; }
        public int Rating { get; set; }

        public SpecialistFilter()
        {
            CityIds = new List<int>();
            ServiceIds = new List<int>();
            LanguageIds = new List<int>();
        }

        public SpecialistFilter(int subCateogryId)
        {
            CityIds = new List<int>();
            ServiceIds = new List<int>();
            LanguageIds = new List<int>();
            SubCategoryId = subCateogryId;
        }

    }
}
