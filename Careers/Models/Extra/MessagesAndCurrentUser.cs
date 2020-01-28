using System.Collections.Generic;

namespace Careers.Models.Extra
{
    public class MessagesAndCurrentUser
    {
        public string AuthorImageUrl { get; set; }

        // Info about order state
        public bool IsFinished { get; set; }
        public bool IsHaveSelectedSpeciaslit { get; set; }
        public bool IsSelectedSpecialist { get; set; }

        public string UserId { get; set; }
        public Dialog Dialog { get; set; }
        public MessagesAndCurrentUser() { }
        public MessagesAndCurrentUser(string userId, Dialog dialog)
        {
            UserId = userId;
            Dialog = dialog;
        }

    }
}
