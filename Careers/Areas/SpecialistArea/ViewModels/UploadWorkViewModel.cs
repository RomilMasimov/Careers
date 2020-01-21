using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Areas.SpecialistArea.ViewModels
{
    public class UploadWorkViewModel
    {
        [Required]
        public IFormFile Image { get; set; }
        
        [MaxLength(150)]
        public string Description { get; set; }
    }
}
