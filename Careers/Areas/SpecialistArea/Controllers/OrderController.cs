using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Careers.Areas.SpecialistArea.ViewModels.Order;
using Careers.Helpers;
using Careers.Models;
using Careers.Models.Enums;
using Careers.Models.Extra;
using Careers.Models.Identity;
using Careers.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NUglify.Helpers;

namespace Careers.Areas.SpecialistArea.Controllers
{
    [Area("SpecialistArea")]
    [Authorize(Roles = "admin,specialist")]
    public class OrderController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ISpecialistService _specialistService;
        private readonly IOrderService _orderService;
        private readonly IMessageService _messageService;

        public OrderController(UserManager<AppUser> userManager, ISpecialistService specialistService, IOrderService orderService, IMessageService messageService)
        {
            _userManager = userManager;
            _specialistService = specialistService;
            _orderService = orderService;
            _messageService = messageService;
        }

        public async Task<IActionResult> Index()
        {
            var specialist = await _specialistService.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var orders = await _orderService.FindAllForSpecialistAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var isRu = CultureInfo.CurrentCulture.Name == "ru-RU";
            var model = orders.Select(m => new OrderViewModel
            {
                Id = m.Id,
                IsEnoughMoneyOnBalance = specialist.Balance > 1,
                State = m.State,
                Created = m.Created,
                ServiceDescription = isRu ? m.Service.DescriptionRU : m.Service.DescriptionAZ,
                ClientId = m.ClientId,
                ClientImage = m.Client.ImageUrl,
                ClientFullName = $"{m.Client.Name} {m.Client.Surname}",
            });
            return View(model);
        }


        public async Task<IActionResult> Order(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
            var myOrder = await _specialistService.HaveIThisOrder(userId, id);
            var order = await _orderService.FindDetailedAsync(id);

            if (order == null)
                return RedirectToAction("Error", "Home", new
                {
                    area = "",
                    code = 404,
                    message = "Order not found.",
                    returnArea = "SpecialistArea",
                    returnController = "Order",
                    returnAction = "Index"
                });

            var model = new OrderDetailsViewModel(order, myOrder, specialist.Balance >= 1);
            return View(model);
        }

        public async Task<IActionResult> MyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
            var orders = await _orderService.FindAllBySpecialistAsync(specialist.Id);

            var isRu = CultureInfo.CurrentCulture.Name == "ru-RU";
            var model = orders.Select(m => new OrderViewModel
            {
                Id = m.Id,
                IsMyOrder = true,
                State = m.State,
                Created = m.Created,
                ServiceDescription = isRu ? m.Service.DescriptionRU : m.Service.DescriptionAZ,
                ClientId = m.ClientId,
                ClientImage = m.Client.ImageUrl,
                ClientFullName = $"{m.Client.Name} {m.Client.Surname}",
            });
            return View(model);
        }

        public async Task<IActionResult> ReceiveDialogId(int id)
        {
            var dialog = await _messageService.GetDialogAsync(id);
            return RedirectToAction("Conversation", new { orderid = dialog.UserSpecialistMessage.OrderId });
        }

        public async Task<IActionResult> Conversation(int orderId)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);

            var dialog = await _messageService.GetDialogAsync(specialist.Id, orderId);
            if (dialog == null) return Content("NotFound");
            await _messageService.MarkAsRead(dialog.UserSpecialistMessage.Id, userId);
            var viewModel = new MessagesAndCurrentUser
            {
                UserId = userId,
                Dialog = dialog,
                AuthorImageUrl = specialist.ImageUrl
            };

            return View(viewModel);
        }

        public async Task<IActionResult> GetImage(IFormFile file)
        {
            var imagePath = await FileUploadHelper.UploadAsync(file, ImageOwnerEnum.Image);
            return Json(imagePath);
        }

        public IActionResult RenderMessage(Message msg, string images)
        {
            if (images != null) msg.ImagePaths = JsonSerializer.Deserialize<List<string>>(images);
            if (msg.Text.IsNullOrWhiteSpace() && !msg.ImagePaths.Any()) return Content("");
            return PartialView("_MessagePartial", msg);
        }

        public async Task<IActionResult> CheckNewOrderAsync(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var answer = await _specialistService.IsOrderForMeAsync(id, userId);
            return Json(answer);
        }



    }
}
