﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Careers.Models;

namespace Careers.ViewModels.Spec
{
    public class SpecialistViewModel
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CityName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Rating { get; set; }
        public string About { get; set; }
        public IEnumerable<WhereCanMeetSpecialist> WhereCanMeetList { get; set; }
        public IEnumerable<WhereCanGoSpecialist> WhereCanGoList { get; set; }
        public IEnumerable<SpecialistWork> Works { get; set; }
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
            Id = specialist.Id;
            ImagePath = specialist.ImageUrl;
            Surname = specialist.Surname;
            Name = specialist.Name;
            FatherName = specialist.Fathername;
            DateOfBirth = specialist.DateOfBirth;
            CityName = specialist.City?.Name;
            Phone = specialist.AppUser.PhoneNumber;
            Email = specialist.AppUser.Email;
            Rating = specialist.Rating;
            WhereCanMeetList = specialist.WhereCanMeetList;
            WhereCanGoList = specialist.WhereCanGoList;
            About = specialist.About;
            Works = specialist.SpecialistWorks;
            Languages = specialist.LanguageSpecialists?.Select(x => x.Language.Name).ToList() ?? new List<string>();
            Educations = specialist.Educations?.ToList()?? new List<Education>();
            Experiences = specialist.Experiences?.ToList() ?? new List<Experience>();
            SpecialistServices = specialist.SpecialistServices?.ToList() ?? new List<SpecialistService>();
            if (CultureInfo.CurrentCulture.Name == "ru-RU")
                SubCategories = specialist.SpecialistSubCategories?.Select(x => x.SubCategory.DescriptionRU).ToList() ?? new List<string>();
            else SubCategories = specialist.SpecialistSubCategories?.Select(x => x.SubCategory.DescriptionAZ).ToList() ?? new List<string>();

            if (specialist.Orders.Any())
            {
                Reviews = new List<Review>();
                foreach (var order in specialist.Orders)
                {
                    if (order.Review == null) continue;
                    Reviews.Add(order.Review);
                }
            }
            else
            {
                Reviews= new List<Review>();
            }
        }
    }
}
