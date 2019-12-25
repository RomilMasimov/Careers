using System.Collections.Generic;

namespace Careers.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string TextAZ { get; set; }
        public string TextRU { get; set; }

        public Question Question { get; set; }
        public int QuestionId { get; set; }
        public IEnumerable<AnswerOrder> AnswerOrders { get; set; }
       
        //public IEnumerable<QuestionAnswer> QuestionAnswers { get; set; }
        //public int? NextQuestionId { get; set; }
        //public Question NextQuestion { get; set; }
        //public IEnumerable<SpecialistAnswer> SpecialistAnswers { get; set; }
    }
}
