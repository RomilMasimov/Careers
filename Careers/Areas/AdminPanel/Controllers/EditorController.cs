using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Careers.Areas.AdminPanel.Models.ViewModels;
using Careers.Models;
using Careers.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Careers.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "admin")]
    public class EditorController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IQuestionService questionService;
        private readonly IAnswerService answerService;

        public EditorController(ICategoryService categoryService, IQuestionService questionService, IAnswerService answerService)
        {
            this.categoryService = categoryService;
            this.questionService = questionService;
            this.answerService = answerService;
        }


        public async Task<IActionResult> Categories()
        {
            var model = new CategoryViewModel
            {
                Categories = await categoryService.GetAllCategories()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Categories(CategoryViewModel model)
        {
            await categoryService.InsertAsync(new Category
            {
                DescriptionAZ = model.DescriptionAZ,
                DescriptionRU = model.DescriptionRU
            });

            return RedirectToAction("Categories");
        }

        public async Task<IActionResult> SubCategoriesAsync()
        {
            var categories = await categoryService.GetAllCategories();
            var model = new SubCategoryViewModel
            {
                Categories = categories.Select(x => 
                new SelectListItem(x.DescriptionAZ + " - " + x.DescriptionRU, x.Id.ToString()))
                .ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult SubCategories(SubCategoryViewModel model)
        {
            categoryService.InsertAsync(new SubCategory
            {
                CategoryId = model.CategoryId,
                DescriptionAZ = model.DescriptionAZ,
                DescriptionRU = model.DescriptionRU
            });

            return RedirectToAction("SubCategories");
        }

        public IActionResult Services()
        {
            var model = new ServiceViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Services(ServiceViewModel model)
        {
            await categoryService.InsertAsync(new Service
            {
                SubCategoryId = model.SubCateoryId,
                DescriptionAZ = model.DescriptionAZ,
                DescriptionRU = model.DescriptionRU
            });

            return RedirectToAction("Services");
        }

        public IActionResult Questions()
        {
            var model = new QuestionViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Questions(QuestionViewModel model)
        {
            await questionService.InsertAsync(new Question
            {
                TextAZ = model.TextAZ,
                TextRU = model.TextRU,
                Type = model.Type,
                SubCategoryId = model.SubCategoryId,
                ServiceId = model.ServiceId
            });

            return RedirectToAction("Questions");
        }

        public IActionResult Answers()
        {
            var model = new AnswerViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Answers(AnswerViewModel model)
        {
            await answerService.InsertAsync(new Answer
            {
                TextAZ = model.TextAZ,
                TextRU = model.TextRU,
                QuestionId = model.QuestionId
            });

            return RedirectToAction("Answers");
        }

        public async Task<IActionResult> Measurments()
        {
            var model = new MeasurmentViewModel
            {
                Measurements = await categoryService.FindAllMeasurements()
            };

            return View(model);
        }

        public IActionResult Measurments(MeasurmentViewModel model)
        {
            categoryService.AddMeasurementAsync(new Measurement
            {
                TextAZ = model.TextAZ,
                TextRU = model.TextRU
            });

            return RedirectToAction("Measurments");
        }
    }
}