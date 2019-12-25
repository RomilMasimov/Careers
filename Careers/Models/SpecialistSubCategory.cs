using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Models
{
    public class SpecialistSubCategory
    {
        public int Id { get; set; }
        public Specialist Specialist { get; set; }
        public int SpecialistId { get; set; }
        public SubCategory SubCategory { get; set; }
        public int SubCategoryId { get; set; }
    }
}
