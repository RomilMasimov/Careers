namespace Careers.Models
{
    public class ClientAnswer
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
