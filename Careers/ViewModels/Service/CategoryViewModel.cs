using System.Collections.Generic;
using Careers.Models;

namespace Careers.ViewModels.Service
{
    public class CategoryViewModel
    {
        public string Lang { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<SubCategory> SubCategories { get; set; }
    }
}
