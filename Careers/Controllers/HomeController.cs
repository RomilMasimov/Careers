using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Careers.Models;
using Careers.Services;
using Careers.Services.Interfaces;

namespace Careers.Controllers
{
    public class HomeController : Controller
    {
        private readonly CareersDbService _service;
        private readonly IClientsService _clientsService;

        public HomeController(CareersDbService service, IClientsService clientsService)
        {
            _service = service;
            _clientsService = clientsService;

            //var excel = new ExcelService();
            //var res = excel.GetSubCategories();
            //if(res!=null)
            //_service.addCategories(res);
        }

        public async Task<IActionResult> Index()
        {
            var person = new Person
            {
                Name = "imran",
                Surname = "veliyev",
                Fathername = "vahid",
                Gender = true,
                DateOfBirth = new DateTime(1997, 6, 12),
                Login = "imran",
                Password = "imran",
                Email = "sayrus719@gmail.com",
                Mobile = "+994507190012",

            };
            var client = new Client
            {
                Person = person
            };

            await _clientsService.InsertAsync(client);
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
