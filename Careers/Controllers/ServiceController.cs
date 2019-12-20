using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Careers.Models;
using Careers.Services.Interfaces;
using Careers.ViewModels.Service;
using Microsoft.AspNetCore.Mvc;

namespace Careers.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ICategoryService _categoryService;

        public ServiceController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(string categoryName = "разные")
        {
            var categories = await _categoryService.GetCategoryAndSubCategoriesAsync(categoryName);
            if (categories == null)
            {
                return View(new CategoryViewModel
                {
                    SubCategories = new List<SubCategory>(),
                    CategoryName = "no category"
                });
            }

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