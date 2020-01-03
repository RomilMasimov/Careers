using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.ViewModels.Home
{
    public class SearchViewModel
    {
        public int SubCategoryId { get; set; }
        public int ServiceId { get; set; }
        public int MettingPointId { get; set; }
        public string ServicesInputText { get; set; }
        public string LocationsInputText { get; set; }
    }
}
