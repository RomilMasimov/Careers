namespace Careers.Models
{
    public class AnswerOrder
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public Answer Answer { get; set; }
        public int AnswerId { get; set; }
    }
}
