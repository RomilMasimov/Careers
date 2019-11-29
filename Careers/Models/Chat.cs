using System.Collections.Generic;

namespace Careers.Models
{
    public class Chat
    {
        public IEnumerable<int> Partisipants { get; set; }
        private IEnumerable<Message> Messages { get; set; }
    }
}
