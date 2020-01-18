using System.Collections.Generic;

namespace Careers.Models.Extra
{
    public class MessagesAndCurrentUser
    {
        public string UserId { get; set; }
        public Dialog Dialog { get; set; }
        public MessagesAndCurrentUser() { }
        public MessagesAndCurrentUser(string id, Dialog dialog)
        {
            UserId = id;
            Dialog = dialog;
        }

    }
}
