using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Models
{
    public class Message
    {
        public int Author { get; set; }
        public string Text { get; set; }
        public IEnumerable<string> ImagePaths { get; set; }
        public DateTime DateTime { get; set; }

    }

}
