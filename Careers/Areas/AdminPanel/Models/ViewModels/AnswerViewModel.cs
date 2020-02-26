using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Areas.AdminPanel.Models.ViewModels
{
    public class AnswerViewModel
    {
        public int QuestionId { get;  set; }
        public string TextRU { get;  set; }
        public string TextAZ { get;  set; }

        public List<SelectListItem> Categories { get; set; }
        public SelectList SubCategories { get; set; }
        public SelectList Services { get; set; }
        public SelectList Questions { get; set; }
    }
}
