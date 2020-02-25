using Careers.Models;
using System.Collections.Generic;

namespace Careers.Areas.AdminPanel.Models.ViewModels
{
    public class CategoryViewModel
    {
        public string DescriptionAZ { get; set; }
        public string DescriptionRU { get; set; }
        public IEnumerable<Category> Categories { get; set; }

    }
}
