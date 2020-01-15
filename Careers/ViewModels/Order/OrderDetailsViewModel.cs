using Careers.Models;
using Careers.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.ViewModels.Order
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public OrderStateTypeEnum State { get; set; }
        public bool IsActive { get; set; }
        public int PriceMin { get; set; }
        public int? PriceMax { get; set; }
        public Measurement Measurement { get; set; }
        public string Description { get; set; }
        public IEnumerable<MeetingPoint> MeetingPoint { get; set; }
        public IEnumerable<AnswerOrder> AnswerOrders { get; set; }
        public IEnumerable<ClientAnswer> ClientAnswers { get; set; }

        public Models.Service Service { get; set; }
        public string SpecialistFullName { get; set; }
        public int? SpecialistId { get; set; }

        public OrderDetailsViewModel(Models.Order order)
        {
            Id = order.Id;
            Created = order.Created;
            State = order.State;
            IsActive = order.IsActive;
            PriceMin = order.PriceMin;
            PriceMax = order.PriceMax;
            Measurement = order.Measurement;
            Description = order.Description;
            MeetingPoint = order.OrderMeetingPoints.Select(m => m.MeetingPoint);
            AnswerOrders = order.AnswerOrders;
            ClientAnswers = order.ClientAnswers;
            Service = order.Service;
            SpecialistId = order.SpecialistId;
            SpecialistFullName = $"{order.Specialist?.Name} {order.Specialist?.Surname} {order.Specialist?.Fathername}";
        }
    }
}
