using System;
using System.Collections.Generic;
using Careers.Models.Enums;

namespace Careers.Models
{
    public class Order
    {
        public int Id { get; set; }
        public OrderStateTypeEnum State { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime Created { get; set; }
        public int PriceMin { get; set; }
        public int? PriceMax { get; set; }
        public string ClientLocation { get; set; }
        public string Description { get; set; }

        public Client Client { get; set; }
        public int ClientId { get; set; }

        public Specialist Specialist { get; set; }
        public int? SpecialistId { get; set; }

        public Service Service { get; set; }
        public int ServiceId { get; set; }

        public Measurement Measurement { get; set; }
        public int MeasurementId { get; set; }

        public Review Review { get; set; }
 
        public ICollection<AnswerOrder> AnswerOrders { get; set; }
        public ICollection<ClientAnswer> ClientAnswers { get; set; }
        public ICollection<OrderResponse> OrderResponses { get; set; }
        public ICollection<OrderMeetingPoint> OrderMeetingPoints { get; set; }
        public ICollection<UserSpecialistMessage> UserSpecialistMessages { get; set; }
        public ICollection<OrderSchedule> OrderSchedules { get; set; }  //remove ???
        // public IEnumerable<OrderSpecialist> OrderSpecialists { get; set; }
    }
}
