using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Careers.Areas.Specialist.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult TakeOrders(bool flag)
        {
            return View();
        }

        [HttpPost]
        public IActionResult ReceiveMessages(bool flag)
        {
            return View();
        }

        [HttpPost]
        public IActionResult ReceiveNotifications(bool flag)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contacts()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contacts(object contacts) // Add a ViewModel
        {
            return View();
        }
    }
}