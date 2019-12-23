﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Areas.SpecialistArea.ViewModels
{
    public class EducationViewModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [Required]
        [MaxLength(150)]
        public string StudyPlaceName { get; set; }

        [Required]
        [MaxLength(150)]
        public string Specialization { get; set; }
        public int SpecialistId { get; set; }
    }
}
