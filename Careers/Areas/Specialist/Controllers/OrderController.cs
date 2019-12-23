using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Careers.Models;
using Careers.Models.Identity;
using Careers.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Careers.Areas.SpecialistArea.Controllers
{
    [Area("Specialist")]
    [Authorize(Roles = "admin,specialist")]
    public class OrderController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ISpecialistService _specialistService;
        private readonly IOrderService _orderService;

        public OrderController(UserManager<AppUser> userManager, ISpecialistService specialistService, IOrderService orderService)
        {
            this._userManager = userManager;
            this._specialistService = specialistService;
            this._orderService = orderService;
        }

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

        public async Task<IActionResult> MyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            var orders = await _orderService.FindAllBySpecialistAsync(specialist.Id);
            setImageUrl(specialist);
            return View(orders);
        }

        public async Task<IActionResult> MyResponces()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            var orders = await _orderService.FindAllResponseBySpecialistAsync(specialist.Id);
            setImageUrl(specialist);
            return View(orders);
        }

        private string setImageUrl(Specialist specialist)
        {
            string path = specialist.ImageUrl;
            if (string.IsNullOrWhiteSpace(specialist.ImageUrl))
                path = "N/A";
            ViewData["ImageUrl"] = path;
            return path;
        }
    }
}
