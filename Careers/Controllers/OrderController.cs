using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Careers.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Orders()
        {
            return View();
        }

        public IActionResult Order()
        {
            return View();
        }
    }
}