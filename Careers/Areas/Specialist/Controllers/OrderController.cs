using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Careers.Areas.Specialist.Controllers
{
    [Area("Specialist")]
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ByFilters(object filters) // Add a ViewModel
        {
            return View();
        }

        public IActionResult Order(int id)
        {
            return View();
        }
    }
}
