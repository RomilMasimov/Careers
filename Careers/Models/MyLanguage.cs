using System.Collections.Generic;

namespace Careers.Models
{
    public class MyLanguage
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<LanguageSpecialist> LanguageSpecialists { get; set; }
    }
}