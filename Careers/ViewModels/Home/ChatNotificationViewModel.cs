using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.ViewModels.Home
{
    public class ChatNotificationViewModel
    {
        public int DialogId { get; set; }
        public string Text { get; set; }
        public string Role { get; set; }
    }
}
