using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.ViewModels.Order
{
    public class ReviewGetViewModel
    {
        public int OrderId { get; set; }
        public int SpecialistId { get; set; }
        public string SpecialistFullName { get; set; }
        public string SpecialistImage { get; set; }
    }
}
