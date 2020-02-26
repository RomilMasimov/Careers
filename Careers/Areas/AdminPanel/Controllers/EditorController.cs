using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Careers.Areas.AdminPanel.Models.ViewModels;
using Careers.Models;
using Careers.Models.Enums;
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

        public async Task<IActionResult> Services()
        {
            var categories = new List<Category>();
            categories.Add(new Category { Id = 0, DescriptionRU = "Выберите категорию", DescriptionAZ = "Kateqoriya seçin" });
            categories.AddRange(await categoryService.GetAllCategories());
            var model = new ServiceViewModel
            {
                Categories = categories.Select(x =>
               new SelectListItem(x.DescriptionAZ + " - " + x.DescriptionRU, x.Id.ToString())).ToList(),
                SubCategories = new SelectList(new[] { new { Id = 0, Text = "Выберите подкатегорию" } }, "Id", "Text")
            };
            return View(model);
        }

        public async Task<IActionResult> SubCategoryOptions(int categoryId)
        {
            var subCategories = await categoryService.GetAllSubCategories(categoryId);
            var selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem("Выберите подкатегорию", "0"));
            selectItems.AddRange(subCategories.Select(m => new SelectListItem(m.DescriptionAZ + " - " + m.DescriptionRU, m.Id.ToString())));
            return PartialView("_SelectOptionsPartial", selectItems);
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

        public async Task<IActionResult> Questions()
        {
            var categories = new List<Category>();
            categories.Add(new Category { Id = 0, DescriptionRU = "Выберите категорию", DescriptionAZ = "Kateqoriya seçin" });
            categories.AddRange(await categoryService.GetAllCategories());

            var model = new QuestionViewModel
            {
                Categories = categories.Select(x =>
               new SelectListItem(x.DescriptionAZ + " - " + x.DescriptionRU, x.Id.ToString())).ToList(),
                SubCategories = new SelectList(new[] { new { Id = 0, Text = "Выберите подкатегорию" } }, "Id", "Text"),
                Services = new SelectList(new[] { new { Id = 0, Text = "Выберите услугу" } }, "Id", "Text")
            };

            return View(model);
        }

        public async Task<IActionResult> ServicesOptions(int subCategoryId)
        {
            var isRu = CultureInfo.CurrentCulture.Name == "ru-RU";
            var services = await categoryService.GetServicesAsync(subCategoryId);
            var selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem(isRu ? "Выберите услугу" : "Xidməti seçin", "0"));
            selectItems.AddRange(services.Select(m => new SelectListItem(m.DescriptionAZ + " - " + m.DescriptionRU, m.Id.ToString())));
            return PartialView("_SelectOptionsPartial", selectItems);
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

        [HttpPost]
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