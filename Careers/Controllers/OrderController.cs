using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Careers.Helpers;
using Careers.Models;
using Careers.Models.Enums;
using Careers.Models.Extra;
using Careers.Services.Interfaces;
using Careers.SignalR;
using Careers.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using NUglify.Helpers;

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
        private readonly IReviewService _reviewService;
        private readonly IMessageService _messageService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public OrderController(IOrderService orderService,
            IQuestionService questionService,
            IMessageService messageService,
            IHubContext<NotificationHub> hubContext,
            IClientService clientService,
            ICategoryService categoryService,
            IAnswerService answerService,
            IReviewService reviewService)
        {
            _orderService = orderService;
            _questionService = questionService;
            _messageService = messageService;
            _hubContext = hubContext;
            _clientService = clientService;
            _categoryService = categoryService;
            _answerService = answerService;
            _reviewService = reviewService;
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
                SpecialistImage = m.Specialist?.ImageUrl,
                SpecialistFullName = $"{m.Specialist?.Name} {m.Specialist?.Surname}",
                IsCanBeRated = m.Review != null || m.State != OrderStateTypeEnum.Finished
            });

            return View(model);
        }

        public async Task<IActionResult> ReceiveDialogId(int id)
        {
            var dialog = await _messageService.GetDialogAsync(id);
            return RedirectToAction("Order","Order", new { id = dialog.UserSpecialistMessage.OrderId });
        }

        public async Task<IActionResult> Order(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var client = await _clientService.FindAsync(userId, true);
            Order order = null;
            if (client.Orders.Any(m => m.Id == id))
                order = await _orderService.FindDetailedAsync(id);

            if (order == null)
                return RedirectToAction("Error", "Home", new { code = 404, message = "Order not found.", returnController = "Order", returnAction = "Index" });
            Request.Cookies.TryGetValue("profileImage", out string image);

            var model = new OrderAndChatViewModel(order, image ?? "");
            return View(model);
        }

        public async Task<IActionResult> GetConversation(int id) //OrderId
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dialog = await _messageService.GetDialogAsync(id);
            if (dialog == null) return Content("NotFound");
            await _messageService.MarkAsRead(dialog.UserSpecialistMessage.Id, userId);

            return PartialView("_ConversationPartial", new MessagesAndCurrentUser(userId, dialog));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreatedOrderViewModel model)
        {
            if (model.ServiceId == "0" &&
                model.Description == "" &&
                model.ClientAnswers.Count(x => x.Answer == "") == 0 &&
                model.AnswerIds.Count == 0 ||
                model.SalaryMin == "")
            {
                return Json("error data");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var client = await _clientService.FindAsync(userId);

            var order = new Order
            {
                Created = DateTime.Now,
                State = OrderStateTypeEnum.InSearchOfSpec,
                ClientLocation = model.ClientLocation,
                Client = client,
                ServiceId = int.Parse(model.ServiceId),
                Description = model.Description
            };

            if (!string.IsNullOrEmpty(model.SalaryMin) &&
                int.TryParse(model.SalaryMin, out var min))
                order.PriceMin = min;
            else order.PriceMin = 0;


            if (!string.IsNullOrEmpty(model.SalaryMax) &&
                int.TryParse(model.SalaryMax, out var max))
                order.PriceMax = max;

            var result = await _orderService.InsertAsync(order);

            if (model.OrderMeetingPoints.Any())
            {
                await _orderService.AddMeetingPoints(model.OrderMeetingPoints.Select(x => new OrderMeetingPoint
                {
                    OrderId = order.Id,
                    MeetingPointId = int.Parse(x)
                }));
            }

            if (model.ClientAnswers.Any(x => x.Answer != ""))
            {
                await _answerService.AddInputAnswers(model.ClientAnswers.Where(x => x.Answer != "").Select(x => new ClientAnswer
                {
                    Text = x.Answer,
                    OrderId = order.Id,
                    QuestionId = x.QuestionId
                }));
            }

            if (model.AnswerIds.Any())
            {
                await _answerService.AddAnswersToOrders(model.AnswerIds.Select(int.Parse).ToArray(), order.Id);
            }

            await _hubContext.Clients.All.SendAsync("NewOrder", order.Id);

            return Json(order.Id);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var isRu = CultureInfo.CurrentCulture.Name == "ru-RU";
            var categories = new List<Category>();
            var measurments = await _categoryService.FindAllMeasurements();
            categories.Add(new Category { Id = 0, DescriptionRU = "Выберите категорию", DescriptionAZ = "Kateqoriya seçin" });
            categories.AddRange(await _categoryService.GetAllCategories());
            ViewBag.Categories = new SelectList(categories, "Id", isRu ? "DescriptionRU" : "DescriptionAZ");
            ViewBag.SubCategories = new SelectList(new[] { new { Id = 0, Text = isRu ? "Выберите подкатегорию" : "Alt kateqoriyanı seçin" } }, "Id", "Text");
            ViewBag.Services = new SelectList(new[] { new { Id = 0, Text = isRu ? "Выберите услугу" : "Xidməti seçin" } }, "Id", "Text");
            ViewBag.Measurments = new SelectList(measurments, "Id", isRu ? "TextAZ" : "TextRU");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody]EditedOrderViewModel model)
        {
            if (model.ServiceId == "0" &&
                model.Description == "" &&
                model.ClientAnswers.Count(x => x.Answer == "") == 0 &&
                !model.AnswerIds.Any() ||
                model.SalaryMin == "")
            {
                return Json("error data");
            }

            if (model.SalaryMax != null &&
                int.Parse(model.SalaryMax) < int.Parse(model.SalaryMin))
            {
                return Json("error data");
            }


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var client = await _clientService.FindAsync(userId);
            var order = await _orderService.FindDetailedAsync(int.Parse(model.Id));

            order.ClientLocation = model.ClientLocation;
            order.ServiceId = int.Parse(model.ServiceId);
            order.Description = model.Description;

            if (!string.IsNullOrEmpty(model.SalaryMin) &&
                int.TryParse(model.SalaryMin, out var min))
                order.PriceMin = min;
            else order.PriceMin = 0;


            if (!string.IsNullOrEmpty(model.SalaryMax) &&
                int.TryParse(model.SalaryMax, out var max))
                order.PriceMax = max;
            else order.PriceMax = 0;

            var meetingPoints = model.OrderMeetingPoints.Select(x => new OrderMeetingPoint
            {
                OrderId = order.Id,
                MeetingPointId = int.Parse(x)
            }).ToList();

            if (!order.OrderMeetingPoints.All(x => meetingPoints.Any(y => y.Id == x.Id)))
            {
                order.OrderMeetingPoints = meetingPoints;
            }

            var clientAnswers = model.ClientAnswers
                .Where(x => x.Answer != "").Select(x => new ClientAnswer
                {
                    Text = x.Answer,
                    OrderId = order.Id,
                    QuestionId = x.QuestionId
                }).ToList();


            if (!order.ClientAnswers.All(x => clientAnswers.Any(y => y.Id == x.Id)))
            {
                await _answerService.DeleteInputAnswers(order.ClientAnswers);
                await _answerService.AddInputAnswers(clientAnswers);
            }

            var answers = model.AnswerIds.Select(int.Parse);

            if (!order.AnswerOrders.All(x => answers.Any(y => y == x.Id)))
            {
                await _orderService.UpdateAnswerOrdersAsync(answers, order.Id);
            }

            await _orderService.UpdateAsync(order);
            await _hubContext.Clients.All.SendAsync("NewOrder", order.Id);

            return Json(order.Id);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _orderService.FindDetailedAsync(id);
            var subCategory = await _categoryService
                .GetSubCategoryAsync(order.Service.SubCategoryId);

            var model = new EditedOrderViewModel();
            model.Id = order.Id.ToString();
            model.ClientLocation = order.ClientLocation;
            model.Description = order.Description;
            model.MeasurmentId = order.MeasurementId.ToString();
            model.SalaryMin = order.PriceMin.ToString();
            model.SalaryMax = order.PriceMax.ToString();
            model.OrderMeetingPoints = order.OrderMeetingPoints
                .Select(x => x.MeetingPointId.ToString()).ToList();
            model.ClientAnswers = order.ClientAnswers
                .Select(x => new ClientInputAnswer { QuestionId = x.QuestionId, Answer = x.Text }).ToList();
            model.CategoryId = subCategory.CategoryId.ToString();
            model.SubCategoryId = subCategory.Id.ToString();
            model.ServiceId = order.ServiceId.ToString();
            model.AnswerIds = order.AnswerOrders
                .Select(x => x.AnswerId.ToString()).ToList();

            var isRu = CultureInfo.CurrentCulture.Name == "ru-RU";
            var categories = new List<Category>();
            var measurments = await _categoryService.FindAllMeasurements();
            categories.Add(new Category { Id = 0, DescriptionRU = "Выберите категорию", DescriptionAZ = "Kateqoriya seçin" });
            categories.AddRange(await _categoryService.GetAllCategories());
            ViewBag.Categories = new SelectList(categories, "Id", isRu ? "DescriptionRU" : "DescriptionAZ");
            ViewBag.SubCategories = new SelectList(new[] { new { Id = 0, Text = isRu ? "Выберите подкатегорию" : "Alt kateqoriyanı seçin" } }, "Id", "Text");
            ViewBag.Services = new SelectList(new[] { new { Id = 0, Text = isRu ? "Выберите услугу" : "Xidməti seçin" } }, "Id", "Text");
            ViewBag.Measurments = new SelectList(measurments, "Id", isRu ? "TextRU" : "TextAZ");

            return View(model);
        }

        public async Task<IActionResult> ChangeIsActiveOrder(int id, bool isActive)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var client = await _clientService.FindAsync(userId, true);
            if (client.Orders.Any(m => m.Id == id))
            {
                var order = await _orderService.ChangeIsActiveOrderAsync(id, isActive);
                if (order != null)
                {
                    return Json(true);
                }
            }
            return Json(false);
        }

        [HttpGet]
        public async Task<IActionResult> AddReview(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var client = await _clientService.FindAsync(userId, true);
            var order = client.Orders.FirstOrDefault(m => m.Id == id);

            if (order == null || order.State != OrderStateTypeEnum.Finished || order.Review != null)
            {
                TempData["Status"] = "You can't review this order.";
                return RedirectToAction("Index");
            }

            var model = new ReviewViewModel
            {
                OrderId = order.Id,
                SpecialistId = order.Specialist.Id,
                SpecialistFullName = $"{order.Specialist.Name} {order.Specialist.Surname}",
                SpecialistImage = order.Specialist.ImageUrl
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(ReviewViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var client = await _clientService.FindAsync(userId, true);
            var order = client.Orders.FirstOrDefault(m => m.Id == model.OrderId);

            if (order == null || order.State != OrderStateTypeEnum.Finished)
            {
                TempData["Status"] = "You can't review this order.";
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid && (model.Images == null || model.Images.Count() <= 6))
            {
                await _reviewService.InsertAsync(model.Text, model.Mark, model.OrderId, model.Images);
                return RedirectToAction("Index");
            }
            model.SpecialistId = order.Specialist.Id;
            model.SpecialistFullName = $"{order.Specialist.Name} {order.Specialist.Surname}";
            model.SpecialistImage = order.Specialist.ImageUrl;
            return View(model);

        }

        public async Task<IActionResult> SubCategoryOptions(int categoryId)
        {
            var isRu = CultureInfo.CurrentCulture.Name == "ru-RU";
            var subCategories = await _categoryService.GetAllSubCategories(categoryId);
            var selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem(isRu ? "Выберите подкатегорию" : "Alt kateqoriyanı seçin", 0.ToString()));
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

        public IActionResult RenderMessage(Message msg, string images)
        {
            if (images != null) msg.ImagePaths = JsonSerializer.Deserialize<List<string>>(images);
            if (msg.Text.IsNullOrWhiteSpace() && !msg.ImagePaths.Any()) return Content("");
            return PartialView("_MessagePartial", msg);
        }

        public async Task<IActionResult> GetImage(IFormFile file)
        {
            var imagePath = await FileUploadHelper.UploadAsync(file, ImageOwnerEnum.Image);
            return Json(imagePath);
        }

    }
}