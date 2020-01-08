using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Areas.SpecialistArea.ViewModels
{
    public class EditAdditionallyViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
    }
}
