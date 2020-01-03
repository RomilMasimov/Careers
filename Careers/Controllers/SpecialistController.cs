using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Careers.Models;
using Careers.Services;
using Careers.Services.Interfaces;
using Careers.ViewModels.Spec;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Careers.Controllers
{
    public class SpecialistController : Controller
    {
        private readonly ISpecialistService _specialistService;
        private readonly LanguageService _languageService;
        private readonly LocationService _locationService;
        private readonly ICategoryService _categoryService;

        public SpecialistController(ISpecialistService specialistService, LanguageService languageService,
            LocationService locationService, ICategoryService categoryService)
        {
            _specialistService = specialistService;
            _languageService = languageService;
            _locationService = locationService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public Task<IActionResult> ListOfSpecialists(int subCategoryId, int serviceId, int cityId)
        {
            var listOfSpecialistsViewModel = new ListOfSpecialistsViewModel();

            if (cityId > 0)
                listOfSpecialistsViewModel.Filter.CityIds.Add(cityId);
            if (serviceId > 0)
                listOfSpecialistsViewModel.Filter.ServiceIds.Add(serviceId);
            else if (subCategoryId > 0)
                listOfSpecialistsViewModel.Filter.SubCategoryIds.Add(subCategoryId);
            return ListOfSpecialists(listOfSpecialistsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ListOfSpecialists(ListOfSpecialistsViewModel model, int subCategoryId = 0, int serviceId = 0)
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
                model.Filter.SubCategoryIds = model.SubCategoriesFilter.Where(x => x.Selected).Select(x => x.Id).ToList();
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

            model.Specialists = await _specialistService.GetByFilterAsync(model.Filter);

            return View(model);
            #endregion
        }

        public async Task<IActionResult> Specialist(int id)
        {
            var specialist = await _specialistService.FindAsync(id);
            var model = new SpecialistViewModel(specialist);
            return View(model);
        }

        public IActionResult Chat()
        {
            return View();
        }
    }
}