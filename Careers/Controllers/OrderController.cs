using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Careers.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Careers.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IActionResult> Index(int id)
        {
            //order with responses
            var order = await _orderService.FindAsync(id,true);
            
            return View();
        }
    }
}