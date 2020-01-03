using System.Collections.Generic;
using Careers.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Careers.ViewModels.Spec
{
    public class ListOfSpecialistsViewModel
    {
        public bool IsEmpty { get; set; }
        public SpecialistFilter Filter { get; set; }
        public List<Specialist> Specialists { get; set; }
        public int SelectedCategoryId { get; set; }
        public List<Category> Categories { get; set; }
        public List<Filter> CitiesFilter { get; set; }
        public List<Filter> ServicesFilter { get; set; }
        public List<Filter> SubCategoriesFilter { get; set; }
        public List<SelectListItem> CategoriesFilter { get; set; }
        public List<Filter> LanguagesFilter { get; set; }
        public List<Filter> ExperienceFilter { get; set; }


        public ListOfSpecialistsViewModel()
        {
            IsEmpty = true;
            Filter = new SpecialistFilter();
            //LanguagesFilter = new List<Filter>();
            //CitiesFilter = new List<Filter>();
            //ServicesFilter = new List<Filter>();
            //Specialists = new List<Specialist>();
            //Categories = new List<Category>();
        }
    }
}
