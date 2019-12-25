using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Careers.Models;
using Careers.Services;
using Careers.Services.Interfaces;
using Careers.ViewModels.Service;
using Microsoft.AspNetCore.Mvc;

namespace Careers.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ISpecialistService _specialistService;
        private readonly LocationService _locationService;

        public ServiceController(ICategoryService categoryService,ISpecialistService specialistService,LocationService locationService)
        {
            _categoryService = categoryService;
            _specialistService = specialistService;
            _locationService = locationService;
        }

        public async Task<IActionResult> Index(string categoryName = "разные")
        {
            var categories = await _categoryService.GetCategoryAndSubCategoriesAsync(categoryName);

            if (categories == null) return View(new CategoryViewModel());

            var viewModel = new CategoryViewModel
            {
                SubCategories = categories.SubCategories,
                CategoryName = CultureInfo.CurrentCulture.Name == "ru-RU" ?
                    categories.DescriptionRU : categories.DescriptionAZ
            };

            return View(viewModel);
        }









    }
}