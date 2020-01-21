using Careers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Areas.SpecialistArea.ViewModels
{
    public class EducationAndExperienceViewModel
    {
        public IEnumerable<Education> Educations { get; set; }
        public IEnumerable<Experience> Experiences { get; set; }
    }
}
