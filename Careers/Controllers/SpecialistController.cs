using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Careers.Models;
using Careers.Services;
using Careers.Services.Interfaces;
using Careers.ViewModels.Spec;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> Specialists(SpecialistsViewModel model, int subCategoryId, int serviceId)
        {
            //public List<int> CityIds { get; set; }
            //public List<int> ServiceIds { get; set; }
            //public int SubCategoryId { get; set; }
            //public List<int> LanguageIds { get; set; }
            //public int ExperienceMin { get; set; }
            //public int ExperienceMax { get; set; }
            //public int Rating { get; set; }
            var cities = await _locationService.GetAllCitiesAsync();//
            var languages = await _languageService.GetAllAsync();//
            var categories = await _categoryService.GetAllCategories(true);//
            //var services = model.SubCategoriesFilter == null ? await _categoryService.GetAllServicesAsync() :
            //    await _categoryService.GetServicesBySubCategoryArrAsync(model.SubCategoriesFilter.Where(x => x.Selected).Select(x => x.Id));

            if (model.IsEmpty)
            {
                var services = await _categoryService.GetAllServicesAsync();
                model = new SpecialistsViewModel
                {
                    CitiesFilter = cities.Select(x => new Filter(x.Id, x.Name)),
                    LanguagesFilter = languages.Select(x => new Filter(x.Id, x.Name)),
                    Categories = categories,
                    Category = categories.FirstOrDefault(),
                };

                if (CultureInfo.CurrentCulture.Name == "ru-RU")
                {
                    model.SubCategoriesFilter = categories.FirstOrDefault()
                        .SubCategories.Select(x => new Filter(x.Id, x.DescriptionRU));
                    model.ServicesFilter = services.Select(x => new Filter(x.Id, x.DescriptionRU)).ToList();
                }
                else
                {
                    model.SubCategoriesFilter = categories.FirstOrDefault()
                        .SubCategories.Select(x => new Filter(x.Id, x.DescriptionAZ));
                    model.ServicesFilter = services.Select(x => new Filter(x.Id, x.DescriptionRU)).ToList();
                }

                if (serviceId > 0 && subCategoryId > 0)
                {
                    model.Filter = new SpecialistFilter();
                   

                    model.Filter = new SpecialistFilter(subCategoryId);
                    var selectedCategory = categories.FirstOrDefault(x => x.SubCategories.FirstOrDefault(y => y.Id == subCategoryId) != null);
                    if (CultureInfo.CurrentCulture.Name == "ru-RU")
                        model.SubCategoriesFilter = selectedCategory.SubCategories.Select(x => new Filter(x.Id, x.DescriptionRU)).ToList();
                    else model.SubCategoriesFilter = selectedCategory.SubCategories.Select(x => new Filter(x.Id, x.DescriptionAZ)).ToList();
                    model.SubCategoriesFilter.First(x => x.Id == subCategoryId).Selected = true;
                    model.Category = selectedCategory;
                    model.ServicesFilter = new List<Filter>();
                    if (CultureInfo.CurrentCulture.Name == "ru-RU")
                    {
                        foreach (var item in model.SubCategoriesFilter)
                        {
                            model.ServicesFilter.ToList().AddRange(services.Where(x => x.SubCategoryId == item.Id).Select(x => new Filter(x.Id, x.DescriptionRU)));
                        }
                    }
                    else
                    {
                        foreach (var item in model.SubCategoriesFilter)
                        {
                            model.ServicesFilter.ToList().AddRange(services.Where(x => x.SubCategoryId == item.Id).Select(x => new Filter(x.Id, x.DescriptionAZ)));
                        }
                    }

                    model.ServicesFilter.First(x => x.Id == serviceId).Selected = true;
                    model.Filter.ServiceIds.Add(serviceId);

                }
                else if (subCategoryId > 0)
                {
                    model.Filter = new SpecialistFilter(subCategoryId);
                    var selectedCategory = categories.FirstOrDefault(x => x.SubCategories.FirstOrDefault(y => y.Id == subCategoryId) != null);
                    if (CultureInfo.CurrentCulture.Name == "ru-RU") model.SubCategoriesFilter = selectedCategory.SubCategories.Select(x => new Filter(x.Id, x.DescriptionRU)).ToList();
                    else model.SubCategoriesFilter = selectedCategory.SubCategories.Select(x => new Filter(x.Id, x.DescriptionAZ)).ToList();

                    model.SubCategoriesFilter.First(x => x.Id == subCategoryId).Selected = true;
                    model.Category = selectedCategory;
                    model.ServicesFilter = new List<Filter>();
                    if (CultureInfo.CurrentCulture.Name == "ru-RU")
                    {
                        foreach (var item in model.SubCategoriesFilter)
                        {
                            model.ServicesFilter.ToList().AddRange(services.Where(x => x.SubCategoryId == item.Id).Select(x => new Filter(x.Id, x.DescriptionRU)));
                        }
                    }
                    else
                    {
                        foreach (var item in model.SubCategoriesFilter)
                        {
                            model.ServicesFilter.ToList().AddRange(services.Where(x => x.SubCategoryId == item.Id).Select(x => new Filter(x.Id, x.DescriptionAZ)));
                        }
                    }
                }
                else
                {
                    model.Filter = new SpecialistFilter();
                }

                model.Specialists = await _specialistService.GetByFilterAsync(model.Filter);

            }


            return View(model);
        }

        public IActionResult Specialist()
        {
            return View();
        }

        public IActionResult Chat()
        {
            return View();
        }
    }
}