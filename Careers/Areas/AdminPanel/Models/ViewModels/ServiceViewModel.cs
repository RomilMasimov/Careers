using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Careers.Areas.AdminPanel.Models.ViewModels
{
    public class ServiceViewModel
    {
        public string DescriptionRU { get; set; }
        public string DescriptionAZ { get; set; }
        public int SubCateoryId { get; set; }

        public List<SelectListItem> Categories { get; set; }
        public SelectList SubCategories { get; set; }
    }
}
