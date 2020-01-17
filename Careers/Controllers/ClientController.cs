using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Careers.Helpers;
using Careers.Models.Enums;
using Careers.Models.Identity;
using Careers.Services;
using Careers.Services.Interfaces;
using Careers.ViewModels.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Careers.Controllers
{
    [Authorize(Roles = "admin,client")]
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SenderService _senderService;

        public ClientController(IClientService clientService, UserManager<AppUser> userManager, SenderService senderService)
        {
            _clientService = clientService;
            _userManager = userManager;
            _senderService = senderService;
        }


        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var client = await _clientService.FindAsync(userId, true);

            return View(new ClientViewModel(client));
        }

        [HttpPost]
        public async Task<IActionResult> Index(ClientViewModel input, IFormFile Image)
        {
            //need work with it...
            var user = await _userManager.GetUserAsync(User);
            input.Messages = new List<string>();
            if (input.OldPhoneNumber != input.PhoneNumber)
            {
                
                var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, input.PhoneNumber);

                var callbackUrl = Url.Action(
                    "ConfirmPhoneNumberChange", "Auth",
                    values: new { userId = user.Id, phoneNumber = input.PhoneNumber, code },
                    protocol: Request.Scheme);

                await _senderService.SendEmail(
                    input.OldEmail, "Confirm your phone",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                //  _senderService.SendSms("507190012", "check", code);
                input.Messages.Add("Confirmation link to change phone sent. Please check your phone.");
            }

            //finished
            if (input.OldEmail != input.Email)
            {
                var code = await _userManager.GenerateChangeEmailTokenAsync(user, input.Email);

                var callbackUrl = Url.Action(
                    "ConfirmEmailChange", "Auth",
                    values: new { userId = user.Id, email = input.Email, code },
                    protocol: Request.Scheme);

                await _senderService.SendEmail(
                    input.OldEmail, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                input.Messages.Add("Confirmation link to change email sent. Please check your email.");
            }

            //finished
            if (input.Password != "P@ssword123" && input.OldPassword != "P@ssword123")
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError(string.Empty, "Model state is invalid");

                    return View(input);
                }

                var result = await _userManager.ChangePasswordAsync(user, input.OldPassword, input.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return View(input);
                }
            }

            //finished
            if (Image != null && !System.IO.File.Exists("wwwroot" + input.ImageUrl))
            {
                input.ImageUrl = await FileUploadHelper.UploadAsync(Image, ImageOwnerEnum.Client);
            }

            var client = await _clientService.FindAsync(user.Id);
            await _clientService.UpdateAsync(input.GetClient(client));

            input.Messages.Add("Changes saved");
            return View(input);
        }

        public IActionResult Chat()
        {
            return View();
        }

    }
}