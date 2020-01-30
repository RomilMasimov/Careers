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
using NUglify.Helpers;

namespace Careers.Controllers
{
    public class AuthController : Controller
    {
        private readonly LocationService _locationService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SenderService _senderService;
        private readonly ISpecialistService _specialistService;
        private readonly IClientService _clientService;

        public AuthController(LocationService locationService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, SenderService senderService, ISpecialistService specialistService, IClientService clientService)
        {
            _locationService = locationService;
            _signInManager = signInManager;
            _userManager = userManager;
            _senderService = senderService;
            _specialistService = specialistService;
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult LogIn(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LoginViewModel model, string returnUrl = null)
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
                    if (!returnUrl.IsNullOrWhiteSpace()) return Redirect(returnUrl);
                    return RedirectToAction("RedirectionAfterSignIn");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }


        public async Task<IActionResult> RedirectionAfterSignIn()
        {
            if (User.IsInRole("specialist"))
            {
                var specialist = await _specialistService.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                Response.Cookies.Append("profileImage", specialist.ImageUrl ?? "");
                return RedirectToAction("Index", "Order", new { area = "SpecialistArea" });
            }

            if (User.IsInRole("client"))
            {
                var client = await _clientService.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                Response.Cookies.Append("profileImage", client.ImageUrl ?? "");
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
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
                },
                IsClient = true
            };
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUpClient(ClientRegistrationVm client)
        {
            if (!client.AgreedWithTerms)
            {
                ModelState.AddModelError(string.Empty, "Accept terms and conditions");
                var viewModel = new RegistrationViewModel
                {
                    Specialist = new SpecialistRegistrationVm
                    {
                        Cities = await _locationService.GetAllCitiesAsync()
                    },
                    IsClient = true

                };
                return View("SignUp",viewModel);
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Model state is invalid");
                var viewModel = new RegistrationViewModel
                {
                    Specialist = new SpecialistRegistrationVm
                    {
                        Cities = await _locationService.GetAllCitiesAsync()
                    },
                    IsClient = true
                };
                return View("SignUp",viewModel);
            }
            
            return await clientReg(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUpSpecialist(SpecialistRegistrationVm specialist)
        {
            if (!specialist.AgreedWithTerms)
            {
                ModelState.AddModelError(string.Empty, "Accept terms and conditions");
                var viewModel = new RegistrationViewModel
                {
                    Specialist = new SpecialistRegistrationVm
                    {
                        Cities = await _locationService.GetAllCitiesAsync()
                    },
                    IsClient = false
                };
                return View("SignUp", viewModel);
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Model state is invalid");
                var viewModel = new RegistrationViewModel
                {
                    Specialist = new SpecialistRegistrationVm
                    {
                        Cities = await _locationService.GetAllCitiesAsync()
                    },
                    IsClient = false
                };
                return View("SignUp", viewModel);
            }

            return await specReg(specialist);
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

                await _senderService.SendEmail(clientViewModel.Email, "Confirm your email",
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

            var viewModel = new RegistrationViewModel
            {
                Specialist = new SpecialistRegistrationVm
                {
                    Cities = await _locationService.GetAllCitiesAsync()
                },
                IsClient = true
            };
            return View("SignUp", viewModel);
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

                await _senderService.SendEmail(specialistViewModel.Email, "Confirm your email",
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

            var viewModel = new RegistrationViewModel
            {
                Specialist = new SpecialistRegistrationVm
                {
                    Cities = await _locationService.GetAllCitiesAsync()
                },
                IsClient = false
            };
            return View("SignUp", viewModel);
        }

        public IActionResult ClientForm()
        {
            return PartialView("_ClientRegistrationPartial",new ClientRegistrationVm());
        }

        public async Task<IActionResult> SpecialistForm()
        {

            var specialist = new SpecialistRegistrationVm
            {
                Cities = await _locationService.GetAllCitiesAsync()
            };
            return PartialView("_SpecialistRegistrationPartial", specialist);
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

                await _senderService.SendEmail(Input.Email, "Reset Password",
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
        [ValidateAntiForgeryToken]
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