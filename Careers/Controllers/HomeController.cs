using System;
using System.Threading.Tasks;
using Careers.Services;
using Careers.Services.Interfaces;
using Careers.ViewModels.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Careers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISpecialistService _specialistService;
        private readonly IReviewService _reviewService;
        private readonly Initializer _initializer;

        public HomeController(ISpecialistService specialistService, IReviewService reviewService, Initializer initializer)
        {
            _specialistService = specialistService;
            _reviewService = reviewService;
            _initializer = initializer;

        }

        public async Task<IActionResult> Index()
        {
            //_initializer.Languages();
            //_initializer.CountryAndCity();
            //_initializer.CategorySubCategory();
            //_initializer.Services();
            //_initializer.MeetingPoints();
            //_initializer.Services();
            //_initializer.QuestionAndAnswers();
            //_initializer.Measurements()
            //await _initializer.ClientsAndSpecialistsAsync();;
            var reviews = await _reviewService.GetBestLastReviewsAsync(5);

            var specialists = await _specialistService.GetBestByCategoryAsync(6);
            var viewModel = new IndexViewModel
            {
                Reviews = reviews,
                Specialists = specialists
            };

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