using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Careers.Models;
using Careers.Services;
using Careers.Services.Interfaces;
using Careers.ViewModels.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Careers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISpecialistService _specialistService;
        private readonly IReviewService _reviewService;
        private readonly IMeetingPointService _meetingPointService;
        private readonly ICategoryService _categoryService;
        private readonly Initializer _initializer;

        public HomeController(ISpecialistService specialistService, IReviewService reviewService, IMeetingPointService meetingPointService, ICategoryService categoryService, Initializer initializer)
        {
            _specialistService = specialistService;
            _reviewService = reviewService;
            _meetingPointService = meetingPointService;
            _categoryService = categoryService;
            _initializer = initializer;

        }

        public async Task<IActionResult> Index()
        {
            //_initializer.QuestionAndAnswers();
            //_initializer.Languages();
            //_initializer.CountryAndCity();
            //_initializer.CategorySubCategory();
            //_initializer.Services();
            //_initializer.MeetingPoints();
            //_initializer.Services();
            //_initializer.Measurements();
            //await _initializer.ClientsAndSpecialistsAsync();

            var reviews = await _reviewService.GetBestLastReviewsAsync(5);

            var specialists = await _specialistService.GetBestByCategoryAsync(6);
            var viewModel = new IndexViewModel
            {
                Reviews = reviews,
                Specialists = specialists
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ServicesAndSubCategoriesAutocomplete(string term)
        {
            IEnumerable<Service> services;
            IEnumerable<SubCategory> subCategories;
            List<AutocompleteViewModel> selectServices = new List<AutocompleteViewModel>(); ;
            if (CultureInfo.CurrentCulture.Name == "ru-RU")
            {
                services = await _categoryService.GetAllServicesByRuTextAsync(term);
                subCategories = await _categoryService.GetAllSubCategoriesByRuTextAsync(term);
                selectServices.AddRange(services.Select(m => new AutocompleteViewModel { Id = m.Id, Value = m.DescriptionRU, Label = m.DescriptionRU }));
                selectServices.AddRange(subCategories.Select(m => new AutocompleteViewModel { Id = m.Id, Value = m.DescriptionRU, Label = m.DescriptionRU }));
            }
            else
            {
                services = await _categoryService.GetAllServicesByAzTextAsync(term);
                subCategories = await _categoryService.GetAllSubCategoriesByAzTextAsync(term);
                selectServices.AddRange(services.Select(m => new AutocompleteViewModel { Id = m.Id, Value = m.DescriptionAZ, Label = m.DescriptionAZ }));
                selectServices.AddRange(subCategories.Select(m => new AutocompleteViewModel { Id = m.Id, Value = m.DescriptionAZ, Label = m.DescriptionAZ }));
            }

            return Json(selectServices);
        }

        [HttpGet]
        public async Task<IActionResult> MeetingPointsAutocomplete(string term)
        {
            IEnumerable<MeetingPoint> meetingPoints = await _meetingPointService.GetAllByTextAsync(term);
            List<AutocompleteViewModel> selectPoints = new List<AutocompleteViewModel>(); ;
            selectPoints.AddRange(meetingPoints.Select(m => new AutocompleteViewModel() { Id = m.Id, Value = m.Description, Label = m.Description }));
            return Json(selectPoints);
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
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