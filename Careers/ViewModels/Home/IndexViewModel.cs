using System.Collections.Generic;
using Careers.Models;

namespace Careers.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<Review> Reviews { get; set; }
        public IEnumerable<Specialist> Specialists { get; set; }
    }
}
