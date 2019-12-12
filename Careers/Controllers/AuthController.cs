using System;
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
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailService _emailService;
        private readonly ISpecialistService _specialistService;
        private readonly IClientService _clientService;

        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, EmailService emailService, ISpecialistService specialistService, IClientService clientService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
            _specialistService = specialistService;
            _clientService = clientService;
        }

        public IActionResult LogIn()
        {
            return View();
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

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegistrationViewModel regViewModel)
        {
            if (!regViewModel.AgreedWithTerms)
            {
                ModelState.AddModelError(string.Empty, "Accept terms and conditions");
                return View();
            }
            if (ModelState.IsValid)
            {
                if (regViewModel.Client != null)
                {
                    var user = new AppUser
                    {
                        UserName = regViewModel.Client.UserName,
                        Email = regViewModel.Client.Email,
                        PhoneNumber = regViewModel.Client.PhoneNumber ?? ""
                    };

                    var result = await _userManager.CreateAsync(user, regViewModel.Client.Password);
                    if (result.Succeeded)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Action(
                            action: "ConfirmEmail",
                            controller: "Auth",
                            values: new { userId = user.Id, code },
                            protocol: Request.Scheme);

                        await _emailService.SendEmail(regViewModel.Client.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        TempData["Email"] = "Please check your Email on new message";

                        await _clientService.InsertAsync(new Client { AppUser = user });

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    var user = new AppUser
                    {
                        UserName = regViewModel.Specialist.UserName,
                        Email = regViewModel.Specialist.Email,
                        PhoneNumber = regViewModel.Specialist.PhoneNumber ?? ""
                    };

                    var result = await _userManager.CreateAsync(user, regViewModel.Specialist.Password);
                    if (result.Succeeded)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Action(
                            action: "ConfirmEmail",
                            controller: "Auth",
                            values: new { userId = user.Id, code },
                            protocol: Request.Scheme);

                        await _emailService.SendEmail(regViewModel.Specialist.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        await _specialistService.InsertAsync(new Specialist { AppUser = user });

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }



    }
}