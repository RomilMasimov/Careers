using System.Collections.Generic;
using Careers.Models.Enums;

namespace Careers.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string TextAZ { get; set; }
        public string TextRU { get; set; }
        public QuestionTypeEnum Type { get; set; }

        public SubCategory SubCategory { get; set; }
        public int SubCategoryId { get; set; }

        public Service Service { get; set; }
        public int? ServiceId { get; set; }

        public IEnumerable<Answer> Answers { get; set; }
        public IEnumerable<ClientAnswer> ClientAnswers { get; set; }

       // public IEnumerable<QuestionAnswer> QuestionAnswers { get; set; }
        //public IEnumerable<Answer> FromAnswers { get; set; }
        //public IEnumerable<DefaultQuestion> DefaultQuestions { get; set; }
    }
}
