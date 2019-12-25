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

        public async Task<IActionResult> Specialists(SpecialistsViewModel model, int subCategoryId, int serviceId)
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

            if (!model.IsEmpty)
            {





                model.Specialists = await _specialistService.GetByFilterAsync(model.Filter);

                return View(model);
            }

            #region category and subCateogry

            if (subCategoryId > 0 && serviceId > 0)
            {
                model = new SpecialistsViewModel
                {
                    CitiesFilter = cities.Select(x => new Filter(x.Id, x.Name)),
                    LanguagesFilter = languages.Select(x => new Filter(x.Id, x.Name)),
                    ExperienceFilter = experience
                };

                model.SelectedCategoryId = categories
                    .FirstOrDefault(x => x.SubCategories.FirstOrDefault(y => y.Id == subCategoryId) != null).Id;

                if (CultureInfo.CurrentCulture.Name == "ru-RU")
                {
                    model.CategoriesFilter = categories.Select(x => new SelectListItem(x.DescriptionRU, x.Id.ToString()));
                    model.SubCategoriesFilter = categories
                        .FirstOrDefault(x => x.Id == model.SelectedCategoryId).SubCategories
                        .Select(x => new Filter(x.Id, x.DescriptionRU)).ToList();
                    model.ServicesFilter = services.Where(x => x.SubCategoryId == subCategoryId)
                        .Select(q => new Filter(q.Id, q.DescriptionRU)).ToList();
                }
                else
                {
                    model.CategoriesFilter = categories.Select(x => new SelectListItem(x.DescriptionAZ, x.Id.ToString()));
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
                model = new SpecialistsViewModel
                {
                    CitiesFilter = cities.Select(x => new Filter(x.Id, x.Name)),
                    LanguagesFilter = languages.Select(x => new Filter(x.Id, x.Name)),
                    ExperienceFilter = experience
                };

                model.SelectedCategoryId = categories
                    .FirstOrDefault(x => x.SubCategories.FirstOrDefault(y => y.Id == subCategoryId) != null).Id;

                if (CultureInfo.CurrentCulture.Name == "ru-RU")
                {
                    model.CategoriesFilter = categories.Select(x => new SelectListItem(x.DescriptionRU, x.Id.ToString()));
                    model.SubCategoriesFilter = categories
                        .FirstOrDefault(x => x.Id == model.SelectedCategoryId).SubCategories
                        .Select(x => new Filter(x.Id, x.DescriptionRU)).ToList();
                    model.ServicesFilter = services.Where(x => x.SubCategoryId == subCategoryId)
                        .Select(q => new Filter(q.Id, q.DescriptionRU)).ToList();
                }
                else
                {
                    model.CategoriesFilter = categories.Select(x => new SelectListItem(x.DescriptionAZ, x.Id.ToString()));
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

            model = new SpecialistsViewModel
            {
                CitiesFilter = cities.Select(x => new Filter(x.Id, x.Name)),
                LanguagesFilter = languages.Select(x => new Filter(x.Id, x.Name)),
                ExperienceFilter = experience
            };

            model.SelectedCategoryId = categories.FirstOrDefault().Id;

            if (CultureInfo.CurrentCulture.Name == "ru-RU")
            {
                model.CategoriesFilter = categories.Select(x => new SelectListItem(x.DescriptionRU, x.Id.ToString()));
                model.SubCategoriesFilter = categories
                    .FirstOrDefault(x => x.Id == model.SelectedCategoryId).SubCategories
                    .Select(x => new Filter(x.Id, x.DescriptionRU));
                model.ServicesFilter = services.Where(x => model.SubCategoriesFilter
                        .Select(y => y.Id).Contains(x.Id))
                    .Select(q => new Filter(q.Id, q.DescriptionRU));
            }
            else
            {
                model.CategoriesFilter = categories.Select(x => new SelectListItem(x.DescriptionAZ, x.Id.ToString()));
                model.SubCategoriesFilter = categories
                    .FirstOrDefault(x => x.Id == model.SelectedCategoryId).SubCategories
                    .Select(x => new Filter(x.Id, x.DescriptionAZ));
                model.ServicesFilter = services.Where(x => model.SubCategoriesFilter
                        .Select(y => y.Id).Contains(x.Id))
                    .Select(q => new Filter(q.Id, q.DescriptionAZ));
            }

            model.Filter = new SpecialistFilter();

            model.Specialists = await _specialistService.GetByFilterAsync(model.Filter);

            return View(model);
            #endregion


            //if (model.IsEmpty)
            //{
            //    var services = await _categoryService.GetAllServicesAsync();
            //    model = new SpecialistsViewModel
            //    {
            //        CitiesFilter = cities.Select(x => new Filter(x.Id, x.Name)),
            //        LanguagesFilter = languages.Select(x => new Filter(x.Id, x.Name)),
            //        Categories = categories,
            //        SelectedCategoryId = categories.FirstOrDefault().Id,
            //    };

            //    if (CultureInfo.CurrentCulture.Name == "ru-RU")
            //    {
            //        model.SubCategoriesFilter = categories.FirstOrDefault()
            //            .SubCategories.Select(x => new Filter(x.Id, x.DescriptionRU));
            //        model.ServicesFilter = services.Select(x => new Filter(x.Id, x.DescriptionRU)).ToList();
            //    }
            //    else
            //    {
            //        model.SubCategoriesFilter = categories.FirstOrDefault()
            //            .SubCategories.Select(x => new Filter(x.Id, x.DescriptionAZ));
            //        model.ServicesFilter = services.Select(x => new Filter(x.Id, x.DescriptionRU)).ToList();
            //    }

            //    if (serviceId > 0 && subCategoryId > 0)
            //    {
            //        model.Filter = new SpecialistFilter();


            //        model.Filter = new SpecialistFilter(subCategoryId);
            //        var selectedCategory = categories.FirstOrDefault(x => x.SubCategories.FirstOrDefault(y => y.Id == subCategoryId) != null);
            //        if (CultureInfo.CurrentCulture.Name == "ru-RU")
            //            model.SubCategoriesFilter = selectedCategory.SubCategories.Select(x => new Filter(x.Id, x.DescriptionRU)).ToList();
            //        else model.SubCategoriesFilter = selectedCategory.SubCategories.Select(x => new Filter(x.Id, x.DescriptionAZ)).ToList();
            //        model.SubCategoriesFilter.First(x => x.Id == subCategoryId).Selected = true;
            //        model.SelectedCategoryId = selectedCategory.Id;
            //        model.ServicesFilter = new List<Filter>();
            //        if (CultureInfo.CurrentCulture.Name == "ru-RU")
            //        {
            //            foreach (var item in model.SubCategoriesFilter)
            //            {
            //                model.ServicesFilter.ToList().AddRange(services.Where(x => x.SubCategoryId == item.Id).Select(x => new Filter(x.Id, x.DescriptionRU)));
            //            }
            //        }
            //        else
            //        {
            //            foreach (var item in model.SubCategoriesFilter)
            //            {
            //                model.ServicesFilter.ToList().AddRange(services.Where(x => x.SubCategoryId == item.Id).Select(x => new Filter(x.Id, x.DescriptionAZ)));
            //            }
            //        }

            //        // model.ServicesFilter.First(x => x.Id == serviceId).Selected = true;
            //        model.Filter.ServiceIds.Add(serviceId);

            //    }
            //    else if (subCategoryId > 0)
            //    {
            //        model.Filter = new SpecialistFilter(subCategoryId);
            //        var selectedCategory = categories.FirstOrDefault(x => x.SubCategories.FirstOrDefault(y => y.Id == subCategoryId) != null);
            //        if (CultureInfo.CurrentCulture.Name == "ru-RU") model.SubCategoriesFilter = selectedCategory.SubCategories.Select(x => new Filter(x.Id, x.DescriptionRU)).ToList();
            //        else model.SubCategoriesFilter = selectedCategory.SubCategories.Select(x => new Filter(x.Id, x.DescriptionAZ)).ToList();

            //        model.SubCategoriesFilter.First(x => x.Id == subCategoryId).Selected = true;
            //        model.SelectedCategoryId = selectedCategory.Id;
            //        model.ServicesFilter = new List<Filter>();
            //        if (CultureInfo.CurrentCulture.Name == "ru-RU")
            //        {
            //            foreach (var item in model.SubCategoriesFilter)
            //            {
            //                model.ServicesFilter.ToList().AddRange(services.Where(x => x.SubCategoryId == item.Id).Select(x => new Filter(x.Id, x.DescriptionRU)));
            //            }
            //        }
            //        else
            //        {
            //            foreach (var item in model.SubCategoriesFilter)
            //            {
            //                model.ServicesFilter.ToList().AddRange(services.Where(x => x.SubCategoryId == item.Id).Select(x => new Filter(x.Id, x.DescriptionAZ)));
            //            }
            //        }
            //    }
            //    else
            //    {
            //        model.Filter = new SpecialistFilter();
            //    }

            //    model.Specialists = await _specialistService.GetByFilterAsync(model.Filter);

            //}





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