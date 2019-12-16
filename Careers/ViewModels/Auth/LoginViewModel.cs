using System.ComponentModel.DataAnnotations;

namespace Careers.ViewModels.Auth
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
