﻿using System.Collections.Generic;
using Careers.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Careers.ViewModels.Spec
{
    public class SpecialistsViewModel
    {
        public bool IsEmpty { get; set; }
        public SpecialistFilter Filter { get; set; }
        public IEnumerable<Specialist> Specialists { get; set; }
        public int SelectedCategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Filter> CitiesFilter { get; set; }
        public IEnumerable<Filter> ServicesFilter { get; set; }
        public IEnumerable<Filter> SubCategoriesFilter { get; set; }
        public IEnumerable<SelectListItem> CategoriesFilter { get; set; }
        public IEnumerable<Filter> LanguagesFilter { get; set; }
        public IEnumerable<Filter> ExperienceFilter { get; set; }


        public SpecialistsViewModel()
        {
            IsEmpty = true;
            //LanguagesFilter = new List<Filter>();
            //CitiesFilter = new List<Filter>();
            //ServicesFilter = new List<Filter>();
            //Specialists = new List<Specialist>();
            //Filter = new SpecialistFilter();
            //Categories = new List<Category>();
        }
    }
}
