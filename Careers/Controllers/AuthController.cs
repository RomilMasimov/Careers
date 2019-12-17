using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Careers.Models;
using Careers.Models.Identity;
using Careers.Services;
using Careers.Services.Interfaces;
using Careers.ViewModels.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace Careers.Controllers
{
    public class AuthController : Controller
    {
        private readonly LocationService _locationService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailService _emailService;
        private readonly ISpecialistService _specialistService;
        private readonly IClientService _clientService;

        public AuthController(LocationService locationService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, EmailService emailService, ISpecialistService specialistService, IClientService clientService)
        {
            _locationService = locationService;
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
            _specialistService = specialistService;
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult LogIn(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogInAsync(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded == false)
                {
                    var user = await _userManager.FindByEmailAsync(model.Login);
                    if (user != null)
                        result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                }

                if (result.Succeeded)
                {
                    if (returnUrl == null) return RedirectToAction("Index", "Home");

                    var roles = HttpContext.User.FindAll(ClaimTypes.Role);
                    if (roles.Any(m => m.Value == "specialist")) return RedirectToAction("Index", "Order", new { area = "Specialist" });
                    return RedirectToAction("Index", "Home");
                }
                else ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);

            TempData["Email"] = "Thank you for confirming your email.";
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> SignUp()
        {
            var viewModel = new RegistrationViewModel
            {
                Specialist = new SpecialistRegistrationVm
                {
                    Cities = await _locationService.GetAllCitiesAsync()
                }
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegistrationViewModel regViewModel)
        {
            if (!regViewModel.AgreedWithTerms)
            {
                ModelState.AddModelError(string.Empty, "Accept terms and conditions");
                return View();
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Model state is invalid");
                return View();
            }

            if (regViewModel.Specialist != null)
            {
                return await specReg(regViewModel.Specialist);
            }

            return await clientReg(regViewModel.Client);
        }

        private async Task<IActionResult> clientReg(ClientRegistrationVm clientViewModel)
        {
            var user = new AppUser
            {
                UserName = clientViewModel.UserName,
                Email = clientViewModel.Email,
                PhoneNumber = clientViewModel.PhoneNumber ?? ""
            };

            var result = await _userManager.CreateAsync(user, clientViewModel.Password);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Action(
                    action: "ConfirmEmail",
                    controller: "Auth",
                    values: new { userId = user.Id, code },
                    protocol: Request.Scheme);

                await _emailService.SendEmail(clientViewModel.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                await _clientService.InsertAsync(new Client { AppUser = user });
                await _userManager.AddToRolesAsync(user, new[] { "client" });
                await _signInManager.SignInAsync(user, isPersistent: false);

                TempData["Email"] = "Please check your Email on new message";
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View("SignUp");
        }

        private async Task<IActionResult> specReg(SpecialistRegistrationVm specialistViewModel)
        {
            var user = new AppUser
            {
                UserName = specialistViewModel.UserName,
                Email = specialistViewModel.Email,
                PhoneNumber = specialistViewModel.PhoneNumber ?? ""
            };

            var result = await _userManager.CreateAsync(user, specialistViewModel.Password);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Action(
                    action: "ConfirmEmail",
                    controller: "Auth",
                    values: new { userId = user.Id, code },
                    protocol: Request.Scheme);

                await _emailService.SendEmail(specialistViewModel.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                await _specialistService.InsertAsync(new Specialist
                {
                    AppUser = user,
                    Name = specialistViewModel.Name,
                    Surname = specialistViewModel.Surname,
                    CityId = specialistViewModel.CityId
                });

                await _userManager.AddToRolesAsync(user, new[] { "specialist" });
                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View("SignUp");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel Input)
        {
            if (ModelState.IsValid)
            {
                TempData["Email"] = "Please check your email to reset your password.";

                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
                {
                    return RedirectToAction("Index", "Home");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Action("ResetPassword", "Auth",
                    values: new { code }, protocol: Request.Scheme);

                await _emailService.SendEmail(Input.Email, "Reset Password",
                    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }

            var Input = new ResetPasswordViewModel
            {
                Code = code
            };
            return View(Input);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel Input)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            TempData["Email"] = "Your password has been reset. Please log in.";

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(Input.Code));

            var result = await _userManager.ResetPasswordAsync(user, code, Input.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

        public async Task<IActionResult> ConfirmEmailChange(string userId, string email, string code)
        {
            string message = "";
            if (userId == null || email == null || code == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ChangeEmailAsync(user, email, code);
            if (!result.Succeeded)
            {
                message = "Error changing email.";
                return View(model: message);
            }

            await _signInManager.RefreshSignInAsync(user);
            message = "Thank you for confirming your email change.";
            return View(model: message);
        }

        public async Task<IActionResult> ConfirmPhoneNumberChange(string userId, string phoneNumber, string code)
        {
            string message = "";
            if (userId == null || phoneNumber == null || code == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ChangePhoneNumberAsync(user, phoneNumber, code);
            if (!result.Succeeded)
            {
                message = "Error changing phone.";
                return View(model: message);
            }

            await _signInManager.RefreshSignInAsync(user);
            message = "Thank you for confirming your phone change.";
            return View(model: message);
        }
    }
}