using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly ICategoryService _categoryService;
        private readonly LocationService _locationService;
        private readonly Initializer _initializer;

        public HomeController(ISpecialistService specialistService, IReviewService reviewService, ICategoryService categoryService, LocationService locationService, Initializer initializer)
        {
            _specialistService = specialistService;
            _reviewService = reviewService;
            _categoryService = categoryService;
            _locationService = locationService;
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
            var cities = await _locationService.GetAllCitiesAsync();
            ViewBag.Locations = new SelectList(cities, "Id", "Name");

            var isRu = CultureInfo.CurrentCulture.Name == "ru-RU";
            var services = await _categoryService.GetAllServicesAsync();
            ViewBag.Services = new SelectList(services, "Id", isRu ? "DescriptionRU" : "DescriptionAZ");
            return View(viewModel);
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