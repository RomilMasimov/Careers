using System.Collections.Generic;

namespace Careers.Models
{
    //json for message and comments
    public class Dialog
    {
        public UserSpecialistMessage UserSpecialistMessage { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
