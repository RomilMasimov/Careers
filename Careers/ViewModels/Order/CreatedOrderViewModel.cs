﻿using System.Collections.Generic;

namespace Careers.ViewModels.Order
{
    public class CreatedOrderViewModel
    {
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string ServiceId { get; set; }
        public string ClientLocation { get; set; }
        public string Description { get; set; }
        public string MeasurmentId { get; set; }
        public string SalaryMin { get; set; }
        public string SalaryMax { get; set; }
        public List<string> AnswerIds { get; set; }
        public List<string> OrderMeetingPoints { get; set; }
        public List<ClientInputAnswer> ClientAnswers { get; set; }

        public CreatedOrderViewModel()
        {
            CategoryId = "0";
            SubCategoryId = "0";
            ServiceId = "0";
        }
    }
}
