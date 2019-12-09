using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Careers.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Careers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IReviewService _reviewService;

        public HomeController(ICategoryService categoryService, IReviewService reviewService)
        {
            _categoryService = categoryService;
            _reviewService = reviewService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategories(true);
            var lastReviews = await _reviewService.GetLastReviewsAsync(4);

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