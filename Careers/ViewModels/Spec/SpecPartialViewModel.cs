using System.Collections.Generic;
using Careers.Models;

namespace Careers.ViewModels.Spec
{
    public class SpecPartialViewModel
    {
        public List<Filter> ServicesFilter { get; set; }
        public List<Specialist>  Specialists { get; set; }
    }
}
