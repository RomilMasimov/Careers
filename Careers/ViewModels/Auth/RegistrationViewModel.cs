using System.ComponentModel.DataAnnotations;

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

        [Display(Name="Phone")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }


    public class SpecialistRegistrationVm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(4, ErrorMessage = "No less than 4 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }

    public class RegistrationViewModel
    {
        public ClientRegistrationVm Client { get; set; }
        public SpecialistRegistrationVm Specialist { get; set; }
        public bool AgreedWithTerms { get; set; }
    }
}
