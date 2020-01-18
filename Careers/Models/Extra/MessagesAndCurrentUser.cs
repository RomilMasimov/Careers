using System.Collections.Generic;

namespace Careers.Models.Extra
{
    public class MessagesAndCurrentUser
    {
        public string UserId { get; set; }
        public IEnumerable<Message> Messages { get; set; }

        public MessagesAndCurrentUser() { }
        public MessagesAndCurrentUser(string id, IEnumerable<Message> messages)
        {
            UserId = id;
            Messages = messages;
        }

    }
}
