using System.Collections.Generic;

namespace Careers.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsGlobalDefaultQuestion { get; set; }

        public Category Category { get; set; }
        public int? CategoryId { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public IEnumerable<Answer> FromAnswers { get; set; }

    }
}
