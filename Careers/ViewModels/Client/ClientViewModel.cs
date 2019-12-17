using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Careers.ViewModels.Client
{
    public class ClientViewModel
    {
        public List<string> Messages { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ImageUrl { get; set; }
        public bool? Gender { get; set; }

        [Display(Name = "Sms notifications")]
        public bool SmsNotifications { get; set; }

        [Display(Name = "Email notifications")]
        public bool EmailNotifications { get; set; }

        public string Email { get; set; }
        public string OldEmail { get; set; }

        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }
        public string OldPhoneNumber { get; set; }

        [Display(Name = "Old password")]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "New password")]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public ClientViewModel() { }

        public ClientViewModel(Models.Client client)
        {
            Messages = new List<string>();
            Name = client.Name;
            Surname = client.Surname;
            ImageUrl = client.ImageUrl;
            Gender = client.Gender;
            SmsNotifications = client.SmsNotifications;
            EmailNotifications = client.EmailNotifications;
            Email = OldEmail = client.AppUser.Email;
            //UserName = client.AppUser.UserName;
            PhoneNumber = OldPhoneNumber = client.AppUser.PhoneNumber;
        }

        public Models.Client GetClient(Models.Client client)
        {
            //reverse
            client.Name = Name;
            client.Surname = Surname;
            client.ImageUrl = ImageUrl;
            client.Gender = Gender;
            client.SmsNotifications = SmsNotifications;
            client.EmailNotifications = EmailNotifications;
            client.AppUser.Email = Email;
            //UserName = client.AppUser.UserName=UserName;
            client.AppUser.PhoneNumber = PhoneNumber;
            return client;
        }


    }
}
