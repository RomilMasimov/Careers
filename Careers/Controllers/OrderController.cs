using System.Security.Claims;
using System.Threading.Tasks;
using Careers.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Careers.Controllers
{
    [Authorize(Roles = "admin,client")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IClientService _clientService;

        public OrderController(IOrderService orderService,IClientService clientService)
        {
            _orderService = orderService;
            _clientService = clientService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var client = await _clientService.FindAsync(userId,true);
            
            return View(client.Orders);
        }


        public IActionResult Create()
        {
            return View();
        }
    }
}