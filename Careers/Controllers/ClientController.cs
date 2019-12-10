﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Careers.Models.Identity;
using Careers.Services.Interfaces;
using IronXL.Xml.Dml.Chart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Careers.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        private readonly UserManager<AppUser> _userManager;

        public ClientController(IClientService clientService, UserManager<AppUser> userManager)
        {
            _clientService = clientService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Profile()
        {

            var user = await _userManager.GetUserAsync(User);
            var client = await _clientService.FindAsync(user.Id);

            return View();
        }

        public async Task<IActionResult> Orders()
        {
          // var userid= User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
           var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //orders are in client
            var client = await _clientService.FindAsync(userId, true);

            return View();
        }

        public async Task<IActionResult> Notifications()
        {
            var user = await _userManager.GetUserAsync(User);
            var client = await _clientService.FindAsync(user.Id);

            return View();
        }


    }
}