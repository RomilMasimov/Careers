using System.Threading.Tasks;
using Careers.Services;
using Careers.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Careers.Controllers
{
    public class HomeController : Controller
    {
        //this will be in a views not in controllers
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public HomeController(IStringLocalizer<SharedResource> sharedLocalizer)
        {
           
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<IActionResult> Index()
        {
            //string localization = _sharedLocalizer["Hello"];
         





            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}