using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.ViewModels.Order
{
    public class ReviewViewModel
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Text { get; set; }

        [Required(ErrorMessage = "Rate")]
        [Range(1, 5, ErrorMessage = "Rate")]
        public int Mark { get; set; }
        public IEnumerable<IFormFile> Images { get; set; }

        public int SpecialistId { get; set; }
        public string SpecialistFullName { get; set; }
        public string SpecialistImage { get; set; }
    }
}
