using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Careers.Models;
using Careers.Models.Enums;
using Careers.Services.Interfaces;
using Careers.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Careers.Controllers
{
    [Authorize(Roles = "admin,client")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IQuestionService _questionService;
        private readonly IClientService _clientService;
        private readonly ICategoryService _categoryService;
        private readonly IAnswerService _answerService;

        public OrderController(IOrderService orderService, IQuestionService questionService,
            IClientService clientService, ICategoryService categoryService, IAnswerService answerService)
        {
            _orderService = orderService;
            _questionService = questionService;
            _clientService = clientService;
            _categoryService = categoryService;
            _answerService = answerService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var client = await _clientService.FindAsync(userId, true);

            var isRu = CultureInfo.CurrentCulture.Name == "ru-RU";

            var model = client.Orders.Select(m => new OrderViewModel
            {
                Id = m.Id,
                State = m.State,
                Created = m.Created,
                ServiceDescription = isRu ? m.Service.DescriptionRU : m.Service.DescriptionAZ,
                SpecialistId = m.SpecialistId,
                SpecialistImage = m.Specialist.ImageUrl,
                SpecialistFullName = $"{m.Specialist.Name} {m.Specialist.Surname}",
            });

            return View(model);
        }

        public IActionResult Order(int id)
        {
            return Content($"I'm order {id}");
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreatedOrderViewModel model)
        {
            if (model.ClientAnswers == null && model.ClientLocation == null && model.Multi == null &&
                model.OrderMeetingPoints == null && model.Description == null && model.SalaryMin == null)
            {
                ModelState.AddModelError("error", "Please at least add description and min price!");
                return View();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var client = await _clientService.FindAsync(userId);

            var order = new Order
            {
                Created = DateTime.Now,
                State = OrderStateTypeEnum.InSearchOfSpec,
                ClientLocation = model.ClientLocation,
                Client = client,
                ServiceId = model.ServiceId,
                Description = model.Description
            };

            if (model.ServiceId == 0) 
                order.ServiceId = (await _categoryService.FindServiceAsync("другое")).Id;

            if (int.TryParse(model.SalaryMin, out var min)) order.PriceMin = min;
            else return StatusCode(StatusCodes.Status500InternalServerError);

            if (model.SalaryMax != null)
            {
                if (int.TryParse(model.SalaryMin, out var max)) order.PriceMax = min;
                else return StatusCode(StatusCodes.Status500InternalServerError);
            }

            await _orderService.InsertAsync(order);

            await _orderService.AddMeetingPoints(model.OrderMeetingPoints.Select(x => new OrderMeetingPoint
            {
                OrderId = order.Id,
                MeetingPointId = int.Parse(x)
            }));

            await _answerService.AddInputAnswersToOrders(model.ClientAnswers.Select(x => new ClientAnswer
            {
                Text = x.Answer,
                OrderId = order.Id,
                QuestionId = x.QuestionId
            }));

            await _answerService.AddAnswersToOrders(model.Single.Select(int.Parse).ToArray(), order.Id);
            await _answerService.AddAnswersToOrders(model.Multi.Select(int.Parse).ToArray(), order.Id);

            return Json(order.Id);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var isRu = CultureInfo.CurrentCulture.Name == "ru-RU";
            var categories = new List<Category>();
            categories.Add(new Category { Id = 0, DescriptionRU = "Выберите категорию", DescriptionAZ = "Kateqoriya seçin" }); //TODO add localization
            categories.AddRange(await _categoryService.GetAllCategories());
            ViewBag.Categories = new SelectList(categories, "Id", isRu ? "DescriptionRU" : "DescriptionAZ");
            ViewBag.SubCategories = new SelectList(new[] { new { Id = 0, Text = isRu ? "Выберите подкатегорию" : "Alt kateqoriyanı seçin" } }, "Id", "Text");
            ViewBag.Services = new SelectList(new[] { new { Id = 0, Text = isRu ? "Выберите услугу" : "Xidməti seçin" } }, "Id", "Text");

            return View();
        }

        public async Task<IActionResult> SubCategoryOptions(int categoryId)
        {
            var isRu = CultureInfo.CurrentCulture.Name == "ru-RU";
            var subCategories = await _categoryService.GetAllSubCategories(categoryId);
            var selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem(isRu ? "Выберите подкатегорию" : "Alt kateqoriyanı seçin", 0.ToString())); //TODO add localization
            selectItems.AddRange(subCategories.Select(m => new SelectListItem(isRu ? m.DescriptionRU : m.DescriptionAZ, m.Id.ToString())));
            return PartialView("_SelectOptionsPartial", selectItems);
        }

        public async Task<IActionResult> ServicesOptions(int subCategoryId)
        {
            var isRu = CultureInfo.CurrentCulture.Name == "ru-RU";
            var services = await _categoryService.GetServicesAsync(subCategoryId);
            var selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem(isRu ? "Выберите услугу" : "Xidməti seçin", 0.ToString())); //TODO add localization
            selectItems.AddRange(services.Select(m => new SelectListItem(isRu ? m.DescriptionRU : m.DescriptionAZ, m.Id.ToString())));
            return PartialView("_SelectOptionsPartial", selectItems);
        }

        public async Task<IActionResult> Questions(int subCategoryId, int? serviceId)
        {
            if (subCategoryId <= 0) return null;

            var questions = await _questionService.FindAllAsync(subCategoryId, serviceId);
            return PartialView("_QuestionsPartial", questions);
        }
    }
}