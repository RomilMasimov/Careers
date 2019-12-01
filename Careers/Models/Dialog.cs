using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Models
{
    //json for message and comments
    public class Dialog
    {
        public Client Client { get; set; }
        public Specialist Specialist { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
