using System.ComponentModel.DataAnnotations;

namespace Careers.ViewModels.Auth
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
