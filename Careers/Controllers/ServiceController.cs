using System.Collections.Generic;
using System.Globalization;
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

      
        public IActionResult Index(string categoryName="others")
        {

            CultureInfo.CurrentCulture = new CultureInfo("az");
            _categoryService.GetCategoryAndSubCategoriesAsync(categoryName);
            var viewModel=new CategoryViewModel{SubCategories = new List<SubCategory>()};
            return View(viewModel);
        }
    }
}