using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Careers.Models;
using Careers.Models.DTO;
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

        public async Task Send(MessageDTO message)
        {
            if (message.ImgPaths?.Count() == 0 && message.Message.IsNullOrWhiteSpace()) return;
            
            var currentUserId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var msg = new Message
            {
                Text = message.Message ?? "",
                Author = currentUserId,
                AuthorImagePath = message.AuthorImageUrl,
                ImagePaths = message.ImgPaths?.ToList() ?? new List<string>()
            };

            await this.Clients.User(message.ReceiverId).SendAsync("ReceiveMessage", msg);
            await _messageService.WriteDialogAsync(message.DialogId,msg );
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
