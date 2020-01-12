using Careers;
using Careers.Models;
using Careers.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.ViewModels.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public OrderStateTypeEnum State { get; set; }
        public string ServiceDescription { get; set; }
        public string SpecialistImage { get; set; }
        public string SpecialistFullName { get; set; }
        public int? SpecialistId { get; set; }
        public bool IsCanBeRated { get; set; }
    }
}
