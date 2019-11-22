namespace Careers.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
       
        public Question AskedQuestion { get; set; }
        public int AskedQuestionId { get; set; }
        public Question NextQuestion { get; set; }
        public int? NextQuestionId { get; set; }
        public Order Order { get; set; }
        public int? OrderId { get; set; }
    }
}
