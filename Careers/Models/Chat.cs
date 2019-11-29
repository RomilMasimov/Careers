using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Models
{
    public class Chat
    {
        public IEnumerable<int> Partisipants { get; set; }
        private IEnumerable<Message> Messages { get; set; }
    }
}
