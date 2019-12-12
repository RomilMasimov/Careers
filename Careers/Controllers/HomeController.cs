using System.Threading.Tasks;
using Careers.Services;
using Careers.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Careers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IReviewService _reviewService;
        private readonly LocationService _locationService;
        private readonly IMeetingPointService _meetingPointService;

        //this will be in a views not in controllers
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public HomeController(ICategoryService categoryService, IReviewService reviewService,
            LocationService locationService, IStringLocalizer<SharedResource> sharedLocalizer,
            IMeetingPointService meetingPointService)
        {
            _categoryService = categoryService;
            _reviewService = reviewService;
            _locationService = locationService;
            _sharedLocalizer = sharedLocalizer;
            _meetingPointService = meetingPointService;
        }

        public async Task<IActionResult> Index()
        {
            //var categories = await _categoryService.GetAllCategories(true);
            //var lastReviews = await _reviewService.GetLastReviewsAsync(4);
            //var cities = await _locationService.GetAllCitiesAsync();
            var meetingPoints = await _meetingPointService.GetAllAsync();
            //string localization = _sharedLocalizer["Hello"];
            var r = User?.Identity?.Name;
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