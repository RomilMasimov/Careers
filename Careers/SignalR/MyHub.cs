using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Careers.Models;
using Careers.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NUglify.Helpers;

namespace Careers.SignalR
{
    [Authorize]
    public class MyHub : Hub
    {
        private readonly IMessageService _messageService;

        public MyHub(IMessageService messageService)
        {
            this._messageService = messageService;
        }

        public async Task Send(int usMessageId, string userId, IEnumerable<string> imgPathes, string message)
        {
            if (imgPathes?.Count() == 0 && message.IsNullOrWhiteSpace()) return;
            
            var currentUserId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var msg = new Message
            {
                Author = currentUserId,
                Text = message ?? "",
                ImagePaths = imgPathes?.ToList() ?? new List<string>()
            };

            await this.Clients.User(userId).SendAsync("ReceiveMessage", msg);
            await _messageService.WriteDialogAsync(usMessageId,msg );
        }

        public string GetConnectionId()
        {
            return Context.UserIdentifier;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Notify", $"{Context.User.Identity.Name} вошел в чат");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("Notify", $"{Context.User.Identity.Name} покинул в чат");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
