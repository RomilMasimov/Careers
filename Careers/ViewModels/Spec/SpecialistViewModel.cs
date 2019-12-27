using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Careers.Models;

namespace Careers.ViewModels.Spec
{
    public class SpecialistViewModel
    {
        public string ImagePath { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CityName { get; set; }
        public int Rating { get; set; }
        public string About { get; set; }
        public List<string> Languages { get; set; }
        public List<Education> Educations { get; set; }
        public List<Experience> Experiences { get; set; }
        public List<string> SubCategories { get; set; }
        public List<SpecialistService> SpecialistServices { get; set; }
        public List<Review> Reviews { get; set; }

        public SpecialistViewModel()
        {
            Languages = new List<string>();
            Educations = new List<Education>();
            Experiences = new List<Experience>();
            SubCategories = new List<string>();
            SpecialistServices = new List<SpecialistService>();
            Reviews = new List<Review>();
        }

        public SpecialistViewModel(Specialist specialist)
        {

            ImagePath = specialist.ImageUrl;
            Surname = specialist.Surname;
            Name = specialist.Name;
            FatherName = specialist.Fathername;
            DateOfBirth = specialist.DateOfBirth;
            CityName = specialist.City.Name;
            Rating = specialist.Rating;
            About = specialist.About;
            Languages = specialist.LanguageSpecialists.Select(x => x.Language.Name).ToList();

            Educations = specialist.Educations.ToList();
            Experiences = specialist.Experiences.ToList();
            SpecialistServices = specialist.SpecialistServices.ToList();
            if (CultureInfo.CurrentCulture.Name == "ru-RU")
                SubCategories = specialist.SpecialistSubCategories.Select(x => x.SubCategory.DescriptionRU).ToList();
            else SubCategories = specialist.SpecialistSubCategories.Select(x => x.SubCategory.DescriptionAZ).ToList();

            var rs = specialist.Orders;

            foreach (var order in rs)
            {
                Reviews.AddRange(order.Reviews);
            }
        }
    }
}
