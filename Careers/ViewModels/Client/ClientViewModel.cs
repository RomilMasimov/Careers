using System;
using System.ComponentModel.DataAnnotations;
using Careers.Models.Identity;

namespace Careers.ViewModels.Client
{
    public class ClientViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ImageUrl { get; set; }
        public bool? Gender { get; set; }
        
        [Display(Name = "Sms notifications")]
        public bool SmsNotifications { get; set; }
        
        [Display(Name = "Email notifications")]
        public bool EmailNotifications { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }

        [Display(Name = "Password")]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
