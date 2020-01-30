using System.Threading.Tasks;
using Careers.Services;
using Microsoft.AspNetCore.Mvc;

namespace Careers.Controllers
{
    public class InitializeController:Controller
    {
        private readonly Initializer _initializer;

        public InitializeController(Initializer initializer)
        {
            _initializer = initializer;
        }

        public async Task<IActionResult> Run()
        {
            _initializer.CountryAndCity();
            _initializer.Languages();
            _initializer.Measurements();
            _initializer.MeetingPoints();
            _initializer.CategorySubCategory();
            _initializer.Services();
            _initializer.QuestionAndAnswers();
            await _initializer.ClientsAndSpecialistsAsync();
            return Json("DONE!!!");
        }
    }
}
