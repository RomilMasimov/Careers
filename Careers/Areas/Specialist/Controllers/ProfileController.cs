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
            specialist.Educations = await _specialistService.FindEducationsBySpecialist(specialist.Id);
            specialist.Experiences = await _specialistService.FindExperiencesBySpecialist(specialist.Id);
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
            var result = await _specialistService.UpdateImage(specialist.Id, Image);
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
                var result = await _specialistService.AddWork(specialist.Id, workViewModel.Image, workViewModel.Description);
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
            var work = await _specialistService.FindWork(id);

            if (work.SpecialistId != specialist.Id)
                return RedirectToAction("Works");

            var model = new EditWorkViewModel { Id = work.Id, ImagePath = work.ImagePath, Description = work.Description };
            setImageUrl(specialist);
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

            var work = await _specialistService.FindWork(id);
            if (work == null || work.SpecialistId != specialist.Id)
                return RedirectToAction("Works");

            var result = await _specialistService.DeleteWork(id);
            if (!result) TempData["Status"] = "File did not delete";
            else TempData["Status"] = "File delete successfully";
            setImageUrl(specialist);
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

        public async Task<IActionResult> EducationsAndExperience()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);

            var model = new EducationAndExperienceViewModel();
            model.Educations = await _specialistService.FindEducationsBySpecialist(specialist.Id);
            model.Experiences = await _specialistService.FindExperiencesBySpecialist(specialist.Id);

            setImageUrl(specialist);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddEducation()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            setImageUrl(specialist);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEducation(EducationViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            if (ModelState.IsValid)
            {
                var education = new Education()
                {
                    StudyPlaceName = model.StudyPlaceName,
                    Specialization = model.Specialization,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    SpecialistId = specialist.Id
                };
                var result = await _specialistService.AddEducation(education);
                if (result != null)
                {
                    TempData["Status"] = "Note has been added";
                    return RedirectToAction("EducationsAndExperience");
                }
            }
            setImageUrl(specialist);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditEducation(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            var education = await _specialistService.FindEducation(id);
            if (education == null || education.SpecialistId != specialist.Id)
                return RedirectToAction("EducationsAndExperience");

            var model = new EducationViewModel
            {
                Id = education.Id,
                StudyPlaceName = education.StudyPlaceName,
                Specialization = education.Specialization,
                StartDate = education.StartDate,
                EndDate = education.EndDate,
                SpecialistId = education.SpecialistId
            };
            setImageUrl(specialist);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEducation(EducationViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            if (ModelState.IsValid)
            {
                var education = new Education()
                {
                    Id = model.Id,
                    StudyPlaceName = model.StudyPlaceName,
                    Specialization = model.Specialization,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    SpecialistId = specialist.Id
                };
                var result = await _specialistService.UpdateEducation(education);
                if (result != null)
                {
                    TempData["Status"] = "Note has been edited";
                    return RedirectToAction("EducationsAndExperience");
                }
            }
            setImageUrl(specialist);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEducation(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            var education = await _specialistService.FindEducation(id);

            if (education == null || education.SpecialistId != specialist.Id)
                return RedirectToAction("EducationsAndExperience");

            var result = await _specialistService.DeleteEducation(id);
            if (!result) TempData["Status"] = "Note did not delete";
            else TempData["Status"] = "Note delete successfully";
            return RedirectToAction("EducationsAndExperience");
        }

        [HttpGet]
        public IActionResult AddExperience()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddExperience(ExperienceViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            if (ModelState.IsValid)
            {
                var experiance = new Experience()
                {
                    CompanyName = model.CompanyName,
                    Position = model.Position,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    SpecialistId = specialist.Id
                };
                var result = await _specialistService.AddExperience(experiance);
                if (result != null)
                {
                    TempData["Status"] = "Note has been added";
                    return RedirectToAction("EducationsAndExperience");
                }
            }
            setImageUrl(specialist);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditExperience(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            var experience = await _specialistService.FindExperience(id);
            if (experience == null || experience.SpecialistId != specialist.Id)
                return RedirectToAction("EducationsAndExperience");
            var model = new ExperienceViewModel
            {
                Id = experience.Id,
                CompanyName = experience.CompanyName,
                Position = experience.Position,
                StartDate = experience.StartDate,
                EndDate = experience.EndDate,
                SpecialistId = experience.SpecialistId
            };
            setImageUrl(specialist);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditExperienceAsync(ExperienceViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            if (ModelState.IsValid)
            {
                var experience = new Experience()
                {
                    Id = model.Id,
                    CompanyName = model.CompanyName,
                    Position = model.Position,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    SpecialistId = specialist.Id
                };
                var result = await _specialistService.UpdateExperience(experience);
                if (result != null)
                {
                    TempData["Status"] = "Note has been edited";
                    return RedirectToAction("EducationsAndExperience");
                }
            }
            setImageUrl(specialist);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExperience(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindByUserAsync(userId);
            var experience = await _specialistService.FindExperience(id);

            if (experience == null || experience.SpecialistId != specialist.Id)
                return RedirectToAction("EducationsAndExperience");

            var result = await _specialistService.DeleteExperience(id);
            if (!result) TempData["Status"] = "Note did not delete";
            else TempData["Status"] = "Note delete successfully";
            return RedirectToAction("EducationsAndExperience");
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
            string path = specialist.ImageUrl;
            if (string.IsNullOrWhiteSpace(specialist.ImageUrl))
                path = "N/A";
            ViewData["ImageUrl"] = path;
            return path;
        }

        public IActionResult Balance()
        {
            return View();
        }
    }
}
