using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Areas.SpecialistArea.ViewModels
{
    public class EditWorkViewModel
    {
        [Required]
        public int Id { get; set; }
        public string ImagePath { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }
    }
}
