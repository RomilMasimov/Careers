using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Areas.SpecialistArea.ViewModels
{
    public class ExperienceViewModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required]
        [MaxLength(150)]
        public string CompanyName { get; set; }

        [Required]
        [MaxLength(150)]
        public string Position { get; set; }
        public int SpecialistId { get; set; }
    }
}
