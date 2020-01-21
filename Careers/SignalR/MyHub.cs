using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Careers.Models;
using Careers.Models.Identity;
using Careers.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Careers.SignalR
{
    [Authorize]
    public class MyHub : Hub
    {
        private readonly IMessageService messageService;

        public MyHub(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        public async Task Send(int usMessageId,string userId,IEnumerable<string> imgPathes, string message)
        {
            await this.Clients.User(userId).SendAsync("ReceiveMessage", message);
            var currentUserId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await messageService.WriteDialogAsync(usMessageId, new Message
            {
                Author = currentUserId,
                Text = message,
                ImagePaths=imgPathes.ToList()
            });
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
