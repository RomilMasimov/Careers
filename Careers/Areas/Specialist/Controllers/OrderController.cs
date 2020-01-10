using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Careers.Areas.SpecialistArea.ViewModels.Order;
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

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.FindAllAsync();

            var isRu = CultureInfo.CurrentCulture.Name == "ru-RU";
            var model = orders.Select(m => new OrderViewModel
            {
                Id = m.Id,
                State = m.State,
                Created = m.Created,
                ServiceDescription = isRu ? m.Service.DescriptionRU : m.Service.DescriptionAZ,
                ClientId = m.ClientId,
                ClientImage = m.Client.ImageUrl,
                ClientFullName = $"{m.Client.Name} {m.Client.Surname}",
            });
            return View(model);
        }

        public IActionResult ByFilters(object filters) // Add a ViewModel
        {
            return View();
        }

        public async Task<IActionResult> Order(int id)
        {
            var order = await _orderService.FindDetailedAsync(id);
            var model = new OrderDetailsViewModel(order);
            return View(model);
        }

        public async Task<IActionResult> MyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            var orders = await _orderService.FindAllBySpecialistAsync(specialist.Id);

            var isRu = CultureInfo.CurrentCulture.Name == "ru-RU";
            var model = orders.Select(m => new OrderViewModel
            {
                Id = m.Id,
                State = m.State,
                Created = m.Created,
                ServiceDescription = isRu ? m.Service.DescriptionRU : m.Service.DescriptionAZ,
                ClientId = m.ClientId,
                ClientImage = m.Client.ImageUrl,
                ClientFullName = $"{m.Client.Name} {m.Client.Surname}",
            });

            setImageUrl(specialist);
            return View(model);
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
