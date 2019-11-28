using System.Collections.Generic;

namespace Careers.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string TextAZ { get; set; }
        public string TextRU { get; set; }

        public SubCategory SubCategory { get; set; }
        public int? SubCategoryId { get; set; }

        public IEnumerable<Answer> Answers { get; set; }
        public IEnumerable<Answer> FromAnswers { get; set; }
        public IEnumerable<QuestionAnswer> QuestionAnswers { get; set; }
        public IEnumerable<DefaultQuestion> DefaultQuestions { get; set; }
    }
}
