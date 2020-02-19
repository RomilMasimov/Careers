using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Careers.Models;
using Careers.Services;
using Careers.Services.Interfaces;
using Careers.ViewModels.Partial;
using Careers.ViewModels.Spec;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Careers.Controllers
{
    [Authorize]
    public class SpecialistController : Controller
    {
        private readonly ISpecialistService _specialistService;
        private readonly IClientService _clientService;
        private readonly IOrderService _orderService;
        private readonly LanguageService _languageService;
        private readonly LocationService _locationService;
        private readonly ICategoryService _categoryService;
        private readonly IMessageService _messageService;

        public SpecialistController(ISpecialistService specialistService, IClientService clientService, IOrderService orderService, LanguageService languageService,
            LocationService locationService, ICategoryService categoryService, IMessageService messageService)
        {
            _specialistService = specialistService;
            _clientService = clientService;
            _orderService = orderService;
            _languageService = languageService;
            _locationService = locationService;
            _categoryService = categoryService;
            _messageService = messageService;
        }


        public async Task<IActionResult> ListOfSpecialists(ListOfSpecialistsViewModel model, int cityId = 0, int subCategoryId = 0, int serviceId = 0)
        {
            var cities = await _locationService.GetAllCitiesAsync();
            var languages = await _languageService.GetAllAsync();
            var categories = await _categoryService.GetAllCategories(true);
            var services = await _categoryService.GetAllServicesAsync();
            var experience = new List<Filter>
            {
                new Filter(1,"1 >"),
                new Filter(2,"1 < 2"),
                new Filter(5,"2 < 5"),
                new Filter(6,"5 < "),
            };

            #region has filter

            if (!model.IsEmpty)
            {
                if (CultureInfo.CurrentCulture.Name == "ru-RU")
                {
                    model.CategoriesFilter = categories.Select(x => new SelectListItem(x.DescriptionRU, x.Id.ToString())).ToList();
                }
                else
                {
                    model.CategoriesFilter = categories.Select(x => new SelectListItem(x.DescriptionAZ, x.Id.ToString())).ToList();
                }

                model.Filter.CityIds = model.CitiesFilter.Where(x => x.Selected).Select(x => x.Id).ToList();
                model.Filter.SubCategoryIds = model.SubCategoriesFilter?.Where(x => x.Selected).Select(x => x.Id).ToList() ?? new List<int>();
                model.Filter.ServiceIds = model.ServicesFilter.Where(x => x.Selected).Select(x => x.Id).ToList();
                model.Filter.LanguageIds = model.LanguagesFilter.Where(x => x.Selected).Select(x => x.Id).ToList();

                model.Specialists = await _specialistService.GetByFilterAsync(model.Filter);

                return View(model);
            }
            #endregion

            #region service and subCateogry

            if (subCategoryId > 0 && serviceId > 0)
            {
                model = new ListOfSpecialistsViewModel
                {
                    CitiesFilter = cities.Select(x => new Filter(x.Id, x.Name)).ToList(),
                    LanguagesFilter = languages.Select(x => new Filter(x.Id, x.Name)).ToList(),
                    ExperienceFilter = experience
                };

                model.SelectedCategoryId = categories
                    .FirstOrDefault(x => x.SubCategories.FirstOrDefault(y => y.Id == subCategoryId) != null).Id;

                if (CultureInfo.CurrentCulture.Name == "ru-RU")
                {
                    model.CategoriesFilter = categories.Select(x => new SelectListItem(x.DescriptionRU, x.Id.ToString())).ToList();
                    model.SubCategoriesFilter = categories
                        .FirstOrDefault(x => x.Id == model.SelectedCategoryId).SubCategories
                        .Select(x => new Filter(x.Id, x.DescriptionRU)).ToList();
                    model.ServicesFilter = services.Where(x => x.SubCategoryId == subCategoryId)
                        .Select(q => new Filter(q.Id, q.DescriptionRU)).ToList();
                }
                else
                {
                    model.CategoriesFilter = categories.Select(x => new SelectListItem(x.DescriptionAZ, x.Id.ToString())).ToList();
                    model.SubCategoriesFilter = categories
                        .FirstOrDefault(x => x.Id == model.SelectedCategoryId).SubCategories
                        .Select(x => new Filter(x.Id, x.DescriptionAZ)).ToList();
                    model.ServicesFilter = services.Where(x => x.SubCategoryId == subCategoryId)
                        .Select(q => new Filter(q.Id, q.DescriptionAZ)).ToList();
                }

                model.SubCategoriesFilter.First(x => x.Id == subCategoryId).Selected = true;
                model.ServicesFilter.FirstOrDefault(x => x.Id == serviceId).Selected = true;

                model.Filter = new SpecialistFilter();
                model.Filter.ServiceIds.Add(serviceId);
                model.Filter.SubCategoryIds.Add(subCategoryId);

                var city2 = model.CitiesFilter.FirstOrDefault(x => x.Id == cityId);
                if (city2 != null)
                {
                    city2.Selected = true;
                    model.Filter.CityIds.Add(cityId);
                }

                model.Specialists = await _specialistService.GetByFilterAsync(model.Filter);

                return View(model);
            }
            #endregion

            #region subCategory

            if (subCategoryId > 0)
            {
                model = new ListOfSpecialistsViewModel
                {
                    CitiesFilter = cities.Select(x => new Filter(x.Id, x.Name)).ToList(),
                    LanguagesFilter = languages.Select(x => new Filter(x.Id, x.Name)).ToList(),
                    ExperienceFilter = experience
                };

                model.SelectedCategoryId = categories
                    .FirstOrDefault(x => x.SubCategories.FirstOrDefault(y => y.Id == subCategoryId) != null).Id;

                if (CultureInfo.CurrentCulture.Name == "ru-RU")
                {
                    model.CategoriesFilter = categories.Select(x => new SelectListItem(x.DescriptionRU, x.Id.ToString())).ToList();
                    model.SubCategoriesFilter = categories
                        .FirstOrDefault(x => x.Id == model.SelectedCategoryId).SubCategories
                        .Select(x => new Filter(x.Id, x.DescriptionRU)).ToList();
                    model.ServicesFilter = services.Where(x => x.SubCategoryId == subCategoryId)
                        .Select(q => new Filter(q.Id, q.DescriptionRU)).ToList();
                }
                else
                {
                    model.CategoriesFilter = categories.Select(x => new SelectListItem(x.DescriptionAZ, x.Id.ToString())).ToList();
                    model.SubCategoriesFilter = categories
                        .FirstOrDefault(x => x.Id == model.SelectedCategoryId).SubCategories
                        .Select(x => new Filter(x.Id, x.DescriptionAZ)).ToList();
                    model.ServicesFilter = services.Where(x => x.SubCategoryId == subCategoryId)
                        .Select(q => new Filter(q.Id, q.DescriptionAZ)).ToList();
                }

                model.SubCategoriesFilter.First(x => x.Id == subCategoryId).Selected = true;

                model.Filter = new SpecialistFilter();
                model.Filter.SubCategoryIds.Add(subCategoryId);

                var city1 = model.CitiesFilter.FirstOrDefault(x => x.Id == cityId);
                if (city1 != null)
                {
                    city1.Selected = true;
                    model.Filter.CityIds.Add(cityId);
                }

                model.Specialists = await _specialistService.GetByFilterAsync(model.Filter);

                return View(model);
            }
            #endregion

            #region default

            model = new ListOfSpecialistsViewModel
            {
                CitiesFilter = cities.Select(x => new Filter(x.Id, x.Name)).ToList(),
                LanguagesFilter = languages.Select(x => new Filter(x.Id, x.Name)).ToList(),
                ExperienceFilter = experience
            };

            model.SelectedCategoryId = categories.FirstOrDefault().Id;

            if (CultureInfo.CurrentCulture.Name == "ru-RU")
            {
                model.CategoriesFilter = categories.Select(x => new SelectListItem(x.DescriptionRU, x.Id.ToString())).ToList();
                model.SubCategoriesFilter = categories
                    .FirstOrDefault(x => x.Id == model.SelectedCategoryId).SubCategories
                    .Select(x => new Filter(x.Id, x.DescriptionRU)).ToList();
                model.ServicesFilter = services.Where(x => model.SubCategoriesFilter
                        .Select(y => y.Id).Contains(x.Id))
                    .Select(q => new Filter(q.Id, q.DescriptionRU)).ToList();
            }
            else
            {
                model.CategoriesFilter = categories.Select(x => new SelectListItem(x.DescriptionAZ, x.Id.ToString())).ToList();
                model.SubCategoriesFilter = categories
                    .FirstOrDefault(x => x.Id == model.SelectedCategoryId).SubCategories
                    .Select(x => new Filter(x.Id, x.DescriptionAZ)).ToList();
                model.ServicesFilter = services.Where(x => model.SubCategoriesFilter
                        .Select(y => y.Id).Contains(x.Id))
                    .Select(q => new Filter(q.Id, q.DescriptionAZ)).ToList();
            }

            model.Filter = new SpecialistFilter();

            var city = model.CitiesFilter.FirstOrDefault(x => x.Id == cityId);
            if (city != null)
            {
                city.Selected = true;
                model.Filter.CityIds.Add(cityId);
            }

            model.Specialists = await _specialistService.GetByFilterAsync(model.Filter);

            return View(model);
            #endregion
        }

        public async Task<IActionResult> Specialist(int id)
        {
            var specialist = await _specialistService.FindDetailedAsync(id);
            if (specialist == null)
                return RedirectToAction("Error", "Home", new { code = 404, message = "Specialist not found.", returnController = "Specialist", returnAction = "ListOfSpecialists" });

            var model = new SpecialistViewModel(specialist);
            return View(model);
        }

        public async Task<ActionResult> ContactWithSpecialist(int id) // specialistId
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var client = await _clientService.FindAsync(userId, true);
            var ordersForSpecialist = await _orderService.FindAllForSpecialistByClientAsync(id, client.Id);

            switch (ordersForSpecialist.Count())
            {
                case 0:
                    return PartialView("_CreateOrderModalPartial");
                case 1:
                    return PartialView("_RedirectToOrderModalPartial",
                        new ChatWithSpecViewModel(ordersForSpecialist.FirstOrDefault().Id, id));
                default:
                    return PartialView("_ChooseOrderModalPartial", ordersForSpecialist);
            }
        }

        public async Task<ActionResult> CreateDialog(int orderId, int specialistId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var client = await _clientService.FindAsync(userId, true);
            var dialog = new UserSpecialistMessage
            {
                ClientId = client.Id,
                SpecialistId = specialistId,
                OrderId = orderId
            };
            await _messageService.WriteDialogAsync(dialog, new Message
            {
                Author = userId,
                Text = $"{client.Name} {client.Surname} предлагает вам сотрудничество!"
            });
            return RedirectToAction("Order", "Order", new { id = orderId });
        }

        public IActionResult Chat()
        {
            return View();
        }
    }
}