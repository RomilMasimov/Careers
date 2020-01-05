using Careers.Models;
using Careers.Models.Enums;
using Careers.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Threading.Tasks;

namespace Careers.ViewComponents
{
    public class QuestionViewComponent : ViewComponent
    {
        private readonly IMeetingPointService _meetingPointService;

        public QuestionViewComponent(IMeetingPointService meetingPointService)
        {
            _meetingPointService = meetingPointService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Question question)
        {
            ViewBag.isRu = CultureInfo.CurrentCulture.Name == "ru-RU";
            switch (question.Type)
            {
                case QuestionTypeEnum.Single:
                    return View("Single", question);
                case QuestionTypeEnum.Multi:
                    return View("Multi", question);
                case QuestionTypeEnum.Input:
                    return View("Input", question);
                case QuestionTypeEnum.Date:
                    return View("Date", question);
                case QuestionTypeEnum.MyLocation:
                    return View("MyLocation", question);
                case QuestionTypeEnum.MeetingPoints:
                    ViewBag.Points = new MultiSelectList(await _meetingPointService.GetAllAsync(), "Id", "Description");
                    return View("MeetingPoints", question);
                default:
                    return Content("");
            }
        }
    }
}
