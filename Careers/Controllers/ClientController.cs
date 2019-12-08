using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Careers.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Notifications()
        {
            return View();
        }






    }
}