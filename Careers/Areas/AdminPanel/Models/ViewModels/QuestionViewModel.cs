using Careers.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Areas.AdminPanel.Models.ViewModels
{
    public class QuestionViewModel
    {
        public string TextAZ { get;  set; }
        public string TextRU { get;  set; }
        public QuestionTypeEnum Type { get;  set; }
        public int SubCategoryId { get;  set; }
        public int? ServiceId { get;  set; }
    }
}
