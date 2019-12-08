using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Careers.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult NewOrder()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }
    }
}