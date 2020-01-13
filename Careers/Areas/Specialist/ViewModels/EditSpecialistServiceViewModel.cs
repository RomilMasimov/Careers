using System;
using System.ComponentModel.DataAnnotations;

namespace Careers.Areas.SpecialistArea.ViewModels
{
    public class EditSpecialistServiceViewModel
    {
        public int SubCategoryId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int PriceMin { get; set; }


        [Range(0, int.MaxValue)]
        public int? PriceMax { get; set; }

        [Required]
        public int SpecialistId { get; set; }

        public string ServiceDescription { get; set; }
        [Required]
        public int ServiceId { get; set; }

        [Required]
        public int MeasurementId { get; set; }
    }
}
