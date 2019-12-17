using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Careers.Models;
using Careers.Models.Identity;
using Careers.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Careers.Areas.SpecialistArea.Controllers
{
    [Area("Specialist")]
    [Authorize(Roles = "admin,specialist")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ISpecialistService _specialistService;

        public ProfileController(UserManager<AppUser> userManager, ISpecialistService specialistService)
        {
            _userManager = userManager;
            _specialistService = specialistService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            setImageUrl(specialist);
            return View(specialist);
        }

        [HttpGet]
        public async Task<IActionResult> UploadPortrait()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            var path = setImageUrl(specialist);
            return View(path as object);
        }
        
        [HttpPost]
        public async Task<IActionResult> UploadPortrait(IFormFile Image)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            var result = await _specialistService.UpdateImage(specialist.Id, Image.OpenReadStream());
            if (!result) TempData["Status"] = "Portrait did not upload";
            else TempData["Status"] = "Portrait sent successfully";
            var path = setImageUrl(specialist);
            return View(path as object);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePortrait()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            var result = await _specialistService.DeleteImage(specialist.Id);
            if (!result) TempData["Status"] = "Portrait did not delete";
            else TempData["Status"] = "Portrait delete successfully";
            return View("UploadPortrait");
        }

        [HttpGet]
        public IActionResult Works()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UploadWork()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadWork(IFormFile Image)
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteWork(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult UploadPassport()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadPassport(IFormFile Image)
        {
            return View();
        }

        public IActionResult Educations()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddEducation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEducation(Education education)
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditEducation(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditEducation(Education education)
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteEducation(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditAbout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditAbout(string text)
        {
            return View();
        }

        public IActionResult WhereCanGo()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddWhereCanGo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddWhereCanGo(int specialistId, MeetingPoint point)
        {
            return View();
        }

        public IActionResult WhereCanMeet()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddWhereCanMeet()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddWhereCanMeet(MeetingPoint point)
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditCity()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditCity(City city)
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditServices()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditServices(object services)  // Add a ViewModel
        {
            return View();
        }

        public IActionResult Settings()
        {
            return View();
        }

        private string setImageUrl(Specialist specialist)
        {
            string path = string.Empty;
            if (!string.IsNullOrWhiteSpace(specialist.ImageUrl))
                path = @$"Media/{specialist.ImageUrl}"; //TODO Сделать по нормально. Забирать путь к папке из сервиса
            ViewData["ImageUrl"] = path;
            return path;
        }
    }
}
