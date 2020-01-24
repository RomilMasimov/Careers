﻿using Careers.Models;
using Careers.Models.Enums;
using System;
using System.Collections.Generic;

namespace Careers.Areas.SpecialistArea.ViewModels.Order
{
    public class OrderDetailsViewModel
    {
        public bool IsMyOrder { get; set; }
        public bool IsEnoughMoneyOnBalance { get; set; }
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public OrderStateTypeEnum State { get; set; }
        public int PriceMin { get; set; }
        public int? PriceMax { get; set; }
        public Measurement Measurement { get; set; }
        public string Description { get; set; }
        public IEnumerable<AnswerOrder> AnswerOrders { get; set; }
        public IEnumerable<ClientAnswer> ClientAnswers { get; set; }

        public Models.Service Service { get; set; }
        public string ClientFullName { get; set; }
        public int? ClientId { get; set; }


        public OrderDetailsViewModel() { }

        public OrderDetailsViewModel(Models.Order order, bool myOrder, bool isEnoughMoneyOnBalance)
        {
            IsMyOrder = myOrder;
            IsEnoughMoneyOnBalance = isEnoughMoneyOnBalance;
            Id = order.Id;
            Created = order.Created;
            State = order.State;
            PriceMin = order.PriceMin;
            PriceMax = order.PriceMax;
            Measurement = order.Measurement;
            Description = order.Description;
            AnswerOrders = order.AnswerOrders;
            ClientAnswers = order.ClientAnswers;
            Service = order.Service;
            ClientId = order.ClientId;
            ClientFullName = $"{order.Client.Name} {order.Client.Surname}";
        }
    }
}
