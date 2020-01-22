using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Careers.Areas.SpecialistArea.ViewModels.Order;
using Careers.Models.Extra;
using Careers.Models.Identity;
using Careers.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> Order(int id,bool myOrder=false)
        {
            var order = await _orderService.FindDetailedAsync(id);

            if (order == null)
                return RedirectToAction("Error", "Home", new { area = "", code = 404, message = "Order not found.", returnArea = "SpecialistArea", returnController = "Order", returnAction = "Index" });

            var model = new OrderDetailsViewModel(order,myOrder);
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
                State = m.State,
                Created = m.Created,
                ServiceDescription = isRu ? m.Service.DescriptionRU : m.Service.DescriptionAZ,
                ClientId = m.ClientId,
                ClientImage = m.Client.ImageUrl,
                ClientFullName = $"{m.Client.Name} {m.Client.Surname}",
            });
            return View(model);
        }

     

        public async Task<IActionResult> Conversation(int orderId)
        {

           //var order=await _orderService.FindAsync(orderId);
           var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           var specialist = await _specialistService.FindAsync(userId);

            var dialog = await _messageService.GetDialogAsync(specialist.Id, orderId);
            if (dialog == null) return Content("NotFound");
            //var userId = dialog.UserSpecialistMessage.Specialist.AppUserId;
            return View("Conversation", new MessagesAndCurrentUser(userId, dialog));
        }

        

    }
}
