using System.Collections.Generic;

namespace Careers.ViewModels.Order
{
    public class CreatedOrderViewModel
    {
        public List<string> Single { get; set; }
        public List<string> Multi { get; set; }
        public List<string> OrderMeetingPoints { get; set; }
        public List<ClientInputAnswer> ClientAnswers { get; set; }
        public string SalaryMin { get; set; }
        public string SalaryMax { get; set; }
        public string Description { get; set; }
        public string ClientLocation { get; set; }
        public int ServiceId { get; set; }
        public int SubCategoryId { get; set; }
    }

    public class ClientInputAnswer
    {
        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }
}
