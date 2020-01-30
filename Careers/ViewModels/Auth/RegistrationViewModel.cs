using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Careers.Models;

namespace Careers.ViewModels.Auth
{
    public class ClientRegistrationVm
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email*")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password*")]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password*")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Login*")]
        public string UserName { get; set; }

        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public bool AgreedWithTerms { get; set; }
    }


    public class SpecialistRegistrationVm
    {

        [Required]
        [Display(Name = "Name*")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Surname*")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Login*")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email*")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password*")]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password*")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "City*")]
        public int CityId { get; set; }
        public bool AgreedWithTerms { get; set; }
        public IEnumerable<City> Cities { get; set; }
    }

    public class RegistrationViewModel
    {
        public ClientRegistrationVm Client { get; set; }
        public SpecialistRegistrationVm Specialist { get; set; }
        public bool? IsClient { get; set; }
    }
}
