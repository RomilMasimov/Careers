using Careers.Models;
using System.Collections;
using System.Collections.Generic;

namespace Careers.Areas.SpecialistArea.ViewModels
{
    public class SelectCategoryViewModel
    {
        public Category Category { get; set; }
        public List<SelectSubCategoryViewModel> SelectSubCategory { get; set; }
    }
}