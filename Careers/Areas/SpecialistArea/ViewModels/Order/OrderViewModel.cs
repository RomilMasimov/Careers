using Careers.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Areas.SpecialistArea.ViewModels.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public OrderStateTypeEnum State { get; set; }
        public string ServiceDescription { get; set; }
        public string ClientImage { get; set; }
        public string ClientFullName { get; set; }
        public int? ClientId { get; set; }
    }
}
