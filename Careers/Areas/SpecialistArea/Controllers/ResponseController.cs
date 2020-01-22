using System.Security.Claims;
using System.Threading.Tasks;
using Careers.Models;
using Careers.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Careers.Areas.SpecialistArea.Controllers
{
    public class ResponseController : Controller
    {
        private readonly ISpecialistService _specialistService;
        private readonly IMessageService _messageService;
        private readonly IClientService _clientService;
        private readonly IOrderService _orderService;

        public ResponseController(ISpecialistService specialistService,
            IMessageService messageService,
            IClientService clientService,
            IOrderService orderService)
        {
            _specialistService = specialistService;
            _messageService = messageService;
            _clientService = clientService;
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
            var order = await _orderService.FindAsync(orderId);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialsit = await _specialistService.FindAsync(userId);

            var dialog = new UserSpecialistMessage
            {
                ClientId = order.ClientId,
                SpecialistId = specialsit.Id,
                OrderId = orderId
            };
            await _messageService.WriteDialogAsync(dialog, new Message
            {
                Author = userId,
                Text = $"{specialsit.Name} {specialsit.Surname} предлагает вам сотрудничество!"
            });
            return RedirectToAction("Order", "Order", new { id = orderId });
        }


    }
}