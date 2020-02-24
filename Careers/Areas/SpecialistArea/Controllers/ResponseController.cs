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
    [Area("SpecialistArea")]
    [Authorize(Roles = "admin,specialist")]
    public class ResponseController : Controller
    {
        private readonly ISpecialistService _specialistService;
        private readonly IMessageService _messageService;
        private readonly IClientService _clientService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderService _orderService;

        public ResponseController(ISpecialistService specialistService,
            IMessageService messageService,
            IClientService clientService,
            UserManager<AppUser> userManager,
            IOrderService orderService)
        {
            _specialistService = specialistService;
            _messageService = messageService;
            _clientService = clientService;
            _userManager = userManager;
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Respond(OrderResponse response)
        {
            return View();
        }

        public async Task<ActionResult> CreateResponse(int orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (!user.EmailConfirmed)
            {
                TempData["Notification"] = "Confirm your email !";
                return RedirectToAction("Index", "Home");
            }

            var myOrder = await _specialistService.HaveIThisOrder(userId, orderId);
            if (myOrder) return RedirectToAction("MyOrders", "Order", new { area = "SpecialistArea" });

            var order = await _orderService.FindAsync(orderId);
            var specialist = await _specialistService.FindAsync(userId);

            if (specialist.Balance > 1)
            {
                specialist.Balance -= 1;
                await _specialistService.UpdateAsync(specialist);
            }

            var dialog = new UserSpecialistMessage
            {
                ClientId = order.ClientId,
                SpecialistId = specialist.Id,
                OrderId = orderId
            };

            await _messageService.WriteDialogAsync(dialog, new Message
            {
                Author = userId,
                Text = $"{specialist.Name} {specialist.Surname} предлагает вам сотрудничество!"
            });

            return RedirectToAction("Conversation", "Order", new { area = "SpecialistArea", orderId });
        }

        public async Task<IActionResult> Responses()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
            var orders = await _orderService.FindAllResponsesAsync(specialist.Id);
            if (orders != null)
            {
                var isRu = CultureInfo.CurrentCulture.Name == "ru-RU";
                var viewModels = orders.Select(m => new OrderViewModel
                {
                    IsMyOrder = true,
                    Id = m.Id,
                    State = m.State,
                    Created = m.Created,
                    ServiceDescription = isRu ? m.Service.DescriptionRU : m.Service.DescriptionAZ,
                    ClientId = m.ClientId,
                    ClientImage = m.Client.ImageUrl,
                    ClientFullName = $"{m.Client.Name} {m.Client.Surname}",
                });
                return View(viewModels);
            }
            return View(null);
        }

    }
}