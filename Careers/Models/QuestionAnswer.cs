namespace Careers.Models
{
    public class QuestionAnswer
    {
        public int  Id { get; set; }
        public Question Question { get; set; }
        public int QuestionId { get; set; }
        public Answer Answer { get; set; }
        public int AnswerId { get; set; }
    }
}
