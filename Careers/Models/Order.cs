using System;
using System.Collections.Generic;
using Careers.Models.Enums;

namespace Careers.Models
{
    public class Order
    {
        public int Id { get; set; }
        public OrderStateTypeEnum State { get; set; }
        public DateTime Created { get; set; }
        public int PriceMin { get; set; }
        public int? PriceMax { get; set; }

        public Client Client { get; set; }
        public int ClientId { get; set; }

        public Specialist Specialist { get; set; }
        public int? SpecialistId { get; set; }

        public Service Service { get; set; }
        public int ServiceId { get; set; }

        public IEnumerable<Review> Reviews { get; set; }
        public IEnumerable<AnswerOrder> AnswerOrders { get; set; }
        public IEnumerable<OrderMeetingPoint> OrderMeetingPoints { get; set; }
        //remove ???
        public IEnumerable<OrderSchedule> OrderSchedules { get; set; }
        public IEnumerable<OrderResponse> OrderResponses { get; set; }
        public IEnumerable<UserSpecialistMessage> UserSpecialistMessages { get; set; }
        // public IEnumerable<OrderSpecialist> OrderSpecialists { get; set; }
    }
}
