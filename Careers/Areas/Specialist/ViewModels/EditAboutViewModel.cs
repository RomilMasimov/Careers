using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Areas.SpecialistArea.ViewModels
{
    public class EditAboutViewModel
    {
        [MaxLength(500)]
        public string Text { get; set; }
    }
}
