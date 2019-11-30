using System.Collections.Generic;

namespace Careers.Models
{
    //json for message and comments
    public class Dialog
    {
        public Specialist Specialist { get; set; }
        public Client Client { get; set; }
        private IEnumerable<Message> Messages { get; set; }
    }
}
