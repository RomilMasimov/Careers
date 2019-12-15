namespace Careers.ViewModels.Auth
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; }
        public string Code { get; internal set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
