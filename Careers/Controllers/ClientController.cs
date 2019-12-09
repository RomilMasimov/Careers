using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Careers.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Careers.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<IActionResult> Profile(int clientId)
        {
            var client = await _clientService.FindAsync(clientId);

            return View();
        }

        public IActionResult Orders()
        {
            //from db? from view? 
            var orders = 1;

            return View();
        }

        public IActionResult Notifications()
        {
            return View();
        }






    }
}