using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Careers.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Careers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IReviewService _reviewService;

        //this will be in a views not in controllers
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public HomeController(ICategoryService categoryService, IReviewService reviewService, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _categoryService = categoryService;
            _reviewService = reviewService;
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategories(true);
            var lastReviews = await _reviewService.GetLastReviewsAsync(4);
            string localization = _sharedLocalizer["Hello"];
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