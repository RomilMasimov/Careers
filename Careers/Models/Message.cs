using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Models
{
    public class Message
    {
        public int Id { get; set; }
        public Person SendFrom { get; set; }
        public int SendFromId { get; set; }
        public Person SendTo { get; set; }
        public int SendToId { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public IEnumerable<MessageMediaPath> MessageMediaPaths { get; set; }
    }
}
