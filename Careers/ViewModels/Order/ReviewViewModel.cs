using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.ViewModels.Order
{
    public class ReviewViewModel
    {
        public int OrderId { get; set; }
        public string Text { get; set; }
        public int Mark { get; set; }
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
