using Careers.Models.Enums;
using System;

namespace Careers.Areas.SpecialistArea.ViewModels.Order
{
    public class OrderViewModel
    {
        public bool IsMyOrder { get; set; }
        public bool IsEnoughMoneyOnBalance { get; set; }
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public OrderStateTypeEnum State { get; set; }
        public string ServiceDescription { get; set; }
        public string ClientImage { get; set; }
        public string ClientFullName { get; set; }
        public int? ClientId { get; set; }
    }
}
