﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Careers.Models.Identity;

namespace Careers.Models
{
    public class Specialist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ImageUrl { get; set; }
        public string Fathername { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string About { get; set; }
        public string PassportPath { get; set; }
        public int Balance { get; set; }
        public DateTime LastVisit { get; set; }
        [NotMapped] public int Rating { get; set; } = 1;

        //settings
        public bool TakeOrders { get; set; }
        public bool SmsNotifications { get; set; }
        public bool EmailNotifications { get; set; }
        //end


        //relationships
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public City City { get; set; }
        public int CityId { get; set; }
        public IEnumerable<LanguageSpecialist> LanguageSpecialists { get; set; }
        public IEnumerable<SpecialistWork> SpecialistWorks { get; set; }
        public IEnumerable<Education> Educations { get; set; }
        public IEnumerable<Experience> Experiences { get; set; }
        public IEnumerable<WhereCanMeetSpecialist> WhereCanMeetList { get; set; }
        public IEnumerable<WhereCanGoSpecialist> WhereCanGoList { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<SpecialistSubCategory> SpecialistSubCategories { get; set; }
        public IEnumerable<SpecialistService> SpecialistServices { get; set; }
        public IEnumerable<UserSpecialistMessage> UserSpecialistMessages { get; set; }
        public IEnumerable<OrderResponse> OrderResponses { get; set; }

        //public IEnumerable<SpecialistAnswer> SpecialistAnswers { get; set; }

        public Specialist()
        {
            ImageUrl = "";
            this.LanguageSpecialists = new List<LanguageSpecialist>();
            this.SpecialistWorks = new List<SpecialistWork>();
            this.Educations = new List<Education>();
            this.Experiences = new List<Experience>();
            this.WhereCanMeetList = new List<WhereCanMeetSpecialist>();
            this.WhereCanGoList = new List<WhereCanGoSpecialist>();
            this.Orders = new List<Order>();
            this.SpecialistSubCategories = new List<SpecialistSubCategory>();
            this.SpecialistServices = new List<SpecialistService>();
            this.UserSpecialistMessages = new List<UserSpecialistMessage>();
            this.OrderResponses = new List<OrderResponse>();
        }
    }
}
