﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Careers.Areas.SpecialistArea.ViewModels;
using Careers.Helpers;
using Careers.Models;
using Careers.Models.Identity;
using Careers.Services;
using Careers.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NUglify.Helpers;

namespace Careers.Areas.SpecialistArea.Controllers
{
    [Area("SpecialistArea")]
    [Authorize(Roles = "admin,specialist")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ISpecialistService _specialistService;
        private readonly IMeetingPointService _meetingPointService;
        private readonly ICategoryService _categoryService;
        private readonly LocationService _locationService;

        public ProfileController(
            UserManager<AppUser> userManager,
            ISpecialistService specialistService,
            IMeetingPointService meetingPointService,
            ICategoryService categoryService,
            LocationService locationService)
        {
            _userManager = userManager;
            _specialistService = specialistService;
            _meetingPointService = meetingPointService;
            this._categoryService = categoryService;
            this._locationService = locationService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId, true);
            specialist.Educations = await _specialistService.FindEducationsBySpecialist(specialist.Id);
            specialist.Experiences = await _specialistService.FindExperiencesBySpecialist(specialist.Id);
            return View(specialist);
        }

        [HttpGet]
        public async Task<IActionResult> UploadPortrait()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
            return View(model: specialist.ImageUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadPortrait(IFormFile Image)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
            if (Image != null)
            {
                specialist.ImageUrl = await FileUploadHelper.UploadAsync(Image, Models.Enums.ImageOwnerEnum.Specialist);
                Response.Cookies.Append("profileImage", specialist.ImageUrl);
            }

            await _specialistService.UpdateAsync(specialist);

            if (!specialist.ImageUrl.IsNullOrWhiteSpace())
            {
                TempData["Status"] = "Portrait sent successfully";
            }
            else TempData["Status"] = "Portrait did not upload";

            return View(model: specialist.ImageUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePortrait()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
            FileUploadHelper.Delete(specialist.ImageUrl);
            specialist.ImageUrl = "";
            Response.Cookies.Append("profileImage", "");
            await _specialistService.UpdateAsync(specialist);

            if (!specialist.ImageUrl.IsNullOrWhiteSpace())
            {
                TempData["Status"] = "Photo did not upload";
            }
            else TempData["Status"] = "Photo sent successfully";
            return View("UploadPortrait");
        }

        [HttpGet]
        public async Task<IActionResult> Works()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
            var works = await _specialistService.FindAllWorks(specialist.Id);
            return View(works);
        }

        [HttpGet]
        public async Task<IActionResult> UploadWork()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadWork(UploadWorkViewModel workViewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
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
            var specialist = await _specialistService.FindAsync(userId);
            var work = await _specialistService.FindWork(id);

            if (work.SpecialistId != specialist.Id)
                return RedirectToAction("Works");

            var model = new EditWorkViewModel { Id = work.Id, ImagePath = work.ImagePath, Description = work.Description };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditWork(EditWorkViewModel workViewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
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
            var specialist = await _specialistService.FindAsync(userId);

            var work = await _specialistService.FindWork(id);
            if (work == null || work.SpecialistId != specialist.Id)
                return RedirectToAction("Works");

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

        public async Task<IActionResult> EducationsAndExperience()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);

            var model = new EducationAndExperienceViewModel();
            model.Educations = await _specialistService.FindEducationsBySpecialist(specialist.Id);
            model.Experiences = await _specialistService.FindExperiencesBySpecialist(specialist.Id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddEducation()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEducation(EducationViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
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
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditEducation(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
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
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEducation(EducationViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
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
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEducation(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
            var education = await _specialistService.FindEducation(id);

            if (education == null || education.SpecialistId != specialist.Id)
                return RedirectToAction("EducationsAndExperience");

            var result = await _specialistService.DeleteEducation(id);
            if (!result) TempData["Status"] = "Note did not delete";
            else TempData["Status"] = "Note delete successfully";
            return RedirectToAction("EducationsAndExperience");
        }

        [HttpGet]
        public async Task<IActionResult> AddExperience()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddExperience(ExperienceViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
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
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditExperience(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
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
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditExperience(ExperienceViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
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
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExperience(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
            var experience = await _specialistService.FindExperience(id);

            if (experience == null || experience.SpecialistId != specialist.Id)
                return RedirectToAction("EducationsAndExperience");

            var result = await _specialistService.DeleteExperience(id);
            if (!result) TempData["Status"] = "Note did not delete";
            else TempData["Status"] = "Note delete successfully";
            return RedirectToAction("EducationsAndExperience");
        }

        [HttpGet]
        public async Task<IActionResult> EditAbout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
            var model = new EditAboutViewModel { Text = specialist.About };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAbout(EditAboutViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
            if (ModelState.IsValid)
            {
                var result = await _specialistService.UpdateAbotAsync(specialist.Id, model.Text);
                if (result != null) return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditWhereCanGo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId, true);

            var meetingPoints = await _meetingPointService.GetAllByCityAsync(specialist.CityId);
            var selectedMeetingPoints = specialist.WhereCanGoList.Select(m => m.WhereCanGo);
            ViewBag.Points = new MultiSelectList(meetingPoints, "Id", "Description", selectedMeetingPoints.Select(m => m.Id));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditWhereCanGo(int[] points)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindWithMeetingPointsAsync(userId);
            var result = await _specialistService.UpdateWhereCanGo(specialist.Id, points);
            if (result)
            {
                TempData["Status"] = "Where can go was edited";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditWhereCanMeet()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindWithMeetingPointsAsync(userId);

            var meetingPoints = await _meetingPointService.GetAllByCityAsync(specialist.CityId);
            var selectedMeetingPoints = specialist.WhereCanMeetList.Select(m => m.WhereCanMeet);
            ViewBag.Points = new MultiSelectList(meetingPoints, "Id", "Description", selectedMeetingPoints.Select(m => m.Id));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditWhereCanMeet(int[] points)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
            var result = await _specialistService.UpdateWhereCanMeet(specialist.Id, points);
            if (result)
            {
                TempData["Status"] = "Where can go was edited";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditCity()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
            var allCities = await _locationService.GetAllCitiesAsync();
            ViewBag.Cities = new SelectList(allCities, "Id", "Name", specialist.CityId);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditCity(int city)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
            var result = await _specialistService.UpdateCity(specialist.Id, city);
            if (result)
            {
                TempData["Status"] = "The city has successfully changed";
                return RedirectToAction("Index");
            }
            var allCities = await _locationService.GetAllCitiesAsync();
            ViewBag.Cities = new SelectList(allCities, "Id", "Name", specialist.CityId);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditSubCategories()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId, true);
            var selectedSubCategories = specialist.SpecialistSubCategories.Select(m => m.SubCategory);
            var allCategories = await _categoryService.GetAllCategories(true);

            var selectCategoriesViewModel = new List<SelectCategoryViewModel>();
            foreach (var category in allCategories)
            {
                var selectCategoryViewModel = new SelectCategoryViewModel();
                selectCategoryViewModel.Category = category;
                selectCategoryViewModel.SelectSubCategory = category.SubCategories.Select(m => new SelectSubCategoryViewModel() { Selected = selectedSubCategories.Contains(m), SubCategory = m }).ToList();
                selectCategoriesViewModel.Add(selectCategoryViewModel);
            }
            return View(selectCategoriesViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubCategories(int[] subCategoriesId)  // Add a ViewModel
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);
            var result = await _specialistService.UpdateSubCategoties(specialist.Id, subCategoriesId);
            if (result)
            {
                TempData["Status"] = "Subcategories was edited";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditServices(int subCategoryId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId, true);
            if (!specialist.SpecialistSubCategories.Any(m => m.SubCategoryId == subCategoryId))
                return RedirectToAction("Index");

            var allServices = await _categoryService.GetServicesAsync(subCategoryId);
            var specialistServices = specialist.SpecialistServices;
            var servicesViewModel = new List<EditServiceViewModel>();
            foreach (var service in allServices)
            {
                var serviceViewModel = new EditServiceViewModel();
                serviceViewModel.Service = service;
                serviceViewModel.SpecialistService = specialistServices.FirstOrDefault(m => m.ServiceId == service.Id);
                servicesViewModel.Add(serviceViewModel);
            }
            return View(servicesViewModel.ToArray());
        }

        [HttpGet]
        public async Task<IActionResult> EditService(int serviceId)  // Add a ViewModel
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId, true);
            var service = await _categoryService.FindServiceAsync(serviceId);
            if (service != null && specialist.SpecialistSubCategories.Any(m => m.SubCategoryId == service.SubCategoryId))
            {
                bool isRu = CultureInfo.CurrentCulture.Name == "ru-RU";
                var specialistService = specialist.SpecialistServices.FirstOrDefault(m => m.ServiceId == service.Id);
                var viewModel = new EditSpecialistServiceViewModel
                {
                    SubCategoryId = service.SubCategoryId,
                    ServiceDescription = isRu ? service.DescriptionRU : service.DescriptionAZ,
                    SpecialistId = specialist.Id,
                    ServiceId = service.Id,
                    PriceMin = specialistService == null ? 0 : specialistService.PriceMin,
                    PriceMax = specialistService?.PriceMax,
                    MeasurementId = specialistService == null ? 0 : specialistService.MeasurementId
                };
                ViewBag.Measurements = new SelectList(await _categoryService.FindAllMeasurements(), "Id", isRu ? "TextRU" : "TextAZ");
                return View(viewModel);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveServiceAsync(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);

            var result = await _categoryService.RemoveFromSpecialistAsync(specialist.Id, id);
            if (!result) { }//logger will be here
            return RedirectToAction("EditServices");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditService(EditSpecialistServiceViewModel model)  // Add a ViewModel
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId, true);
            if (ModelState.IsValid &&
                await _categoryService.FindServiceAsync(model.ServiceId) != null)
            {
                var specialistService = specialist.SpecialistServices.FirstOrDefault(m => m.ServiceId == model.ServiceId);
                if (specialistService != null)
                {
                    specialistService.PriceMin = model.PriceMin;
                    specialistService.PriceMax = model.PriceMax;
                    specialistService.MeasurementId = model.MeasurementId;
                }
                else
                {
                    specialistService = new Models.SpecialistService()
                    {
                        SpecialistId = specialist.Id,
                        ServiceId = model.ServiceId,
                        PriceMin = model.PriceMin,
                        PriceMax = model.PriceMax,
                        MeasurementId = model.MeasurementId
                    };
                }
                await _categoryService.UpdateSpecialistServiceAsync(specialistService);
                TempData["Status"] = "Edited";
                return RedirectToAction("EditServices");
            }
            return View(model);
        }


        public IActionResult Balance()
        {
            return View();
        }

        public async Task<IActionResult> AddBalance()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var specialist = await _specialistService.FindAsync(userId);

            specialist.Balance += 5;
            await _specialistService.UpdateAsync(specialist);
            return View("Balance", specialist.Balance);
        }

        public IActionResult Conversation(int id)
        {




            return View();
        }
    }
}
