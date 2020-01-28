using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Careers.Models;
using Careers.Services.Interfaces;
using Careers.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Careers.SignalR
{
    [Authorize]
    public class MessageNotificationHub : Hub
    {
        private readonly IMessageService _messageService;

        public MessageNotificationHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public override async Task OnConnectedAsync()
        {
            List<UserSpecialistMessage> unreadDialogs;
            IEnumerable<ChatNotificationViewModel> model = null;
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Context.User.IsInRole("client"))
            {
                unreadDialogs = await _messageService.GetUnreadDialogsAsync(userId, "client");
                model = unreadDialogs
                        .Select(m => new ChatNotificationViewModel
                        {
                            DialogId = m.Id,
                            Text = $"From {m.Specialist.Name} {m.Specialist.Surname}",
                            Role = "specialist"
                        });
            }
            else if (Context.User.IsInRole("specialist"))
            {
                unreadDialogs = await _messageService.GetUnreadDialogsAsync(userId, "specialist");
                model = unreadDialogs
                        .Select(m => new ChatNotificationViewModel
                        {
                            DialogId = m.Id,
                            Text = $"From {m.Client.Name} {m.Client.Surname}",
                            Role = "client"
                        });
            }

            await Clients.User(userId).SendAsync("UploadNotifications", model);
            await base.OnConnectedAsync();
        }
    }
}
