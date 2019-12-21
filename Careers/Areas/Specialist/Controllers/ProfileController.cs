using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Careers.Areas.SpecialistArea.ViewModels;
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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
        public async Task<IActionResult> Works()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            var works = await _specialistService.FindAllWorks(specialist.Id);
            setImageUrl(specialist);
            return View(works);
        }

        [HttpGet]
        public async Task<IActionResult> UploadWork()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            setImageUrl(specialist);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadWork(UploadWorkViewModel workViewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            setImageUrl(specialist);
            if (ModelState.IsValid)
            {
                var result = await _specialistService.AddWork(specialist.Id, workViewModel.Image.OpenReadStream(), workViewModel.Description);
                if (result != null)
                {
                    TempData["Status"] = "Work sent successfully";
                    return RedirectToAction("Works");
                }
                TempData["Status"] = "Work did not upload";
            }
            return View(workViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditWork(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            setImageUrl(specialist);
            var work = await _specialistService.FindWork(id);
            var model = new EditWorkViewModel { Id = work.Id, ImagePath = work.ImagePath, Description = work.Description };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditWork(EditWorkViewModel workViewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            setImageUrl(specialist);
            if (ModelState.IsValid)
            {
                var work = await _specialistService.EditWork(workViewModel.Id, workViewModel.Description);
                if (work != null)
                {
                    TempData["Status"] = "Changes saved successfully";
                    return RedirectToAction("Works");
                }
            }
            return View(workViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWork(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            setImageUrl(specialist);

            var result = await _specialistService.DeleteWork(id);
            if (!result) TempData["Status"] = "File did not delete";
            else TempData["Status"] = "File delete successfully";
            return RedirectToAction("Works");
        }

        [HttpGet]
        public IActionResult UploadPassport()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
        public IActionResult EditEducation(Education education)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEducation(int id)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditAboutAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            setImageUrl(specialist);
            var model = new EditAboutViewModel { Text = specialist.About };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAbout(EditAboutViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            setImageUrl(specialist);
            if (ModelState.IsValid)
            {
                var result = await _specialistService.UpdateAbotAsync(specialist.Id, model.Text);
                if (result != null) return RedirectToAction("Index");
            }
            return View(model);
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
