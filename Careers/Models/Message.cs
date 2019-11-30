using System;
using System.Collections.Generic;

namespace Careers.Models
{
    //json for message and comment
    public class Message
    {
        public int Author { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }

        public IEnumerable<string> ImagePaths { get; set; }
    }

}
