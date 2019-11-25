using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Careers.Models;
using Careers.Services;

namespace Careers.Controllers
{
    public class HomeController : Controller
    {
        private readonly CareersDbService _service;

        public HomeController(CareersDbService service)
        {
            _service = service;
            var excel = new ExcelService();
            var res = excel.GetSubCategories();
            if(res!=null)
            _service.addCategories(res);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
