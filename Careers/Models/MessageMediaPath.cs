using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Models
{
    public class MessageMediaPath
    {
        public int Id { get; set; }
        public Message Message { get; set; }
        public int MessageId { get; set; }
        public MediaPath MediaPath { get; set; }
        public int MediaPathId { get; set; }
    }
}
