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
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly ISpecialistService _specialistService;
        private readonly IClientService _clientService;
        private readonly IHubContext<MessageNotificationHub> _hubContext;
        private List<string> onlineUsers;
        public ChatHub(IMessageService messageService,
            ISpecialistService specialistService,
            IClientService clientService,
            IHubContext<MessageNotificationHub> hubContext)
        {
            onlineUsers = new List<string>();
            this._messageService = messageService;
            _specialistService = specialistService;
            _clientService = clientService;
            _hubContext = hubContext;
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
            if (onlineUsers.Contains(currentUserId))
                await this.Clients.User(message.ReceiverId).SendAsync("ReceiveMessage", msg);
            else
            {
                var fullName = "";
                var role = "";
                if (Context.User.IsInRole("client"))
                {
                    role = "client";
                    var client = await _clientService.FindAsync(currentUserId);
                    fullName = client.Name + " " + client.Surname;
                }
                else if (Context.User.IsInRole("specialist"))
                {
                    role = "client";
                    var specialist = await _specialistService.FindAsync(currentUserId);
                    fullName = specialist.Name + " " + specialist.Surname;
                }

                var text = $"From {fullName} {msg.DateTime.ToShortTimeString()}";
                await _hubContext.Clients.User(message.ReceiverId).SendAsync("NewMsgCame", new{dialogId=message.DialogId,text,role});
            }

            await _messageService.WriteDialogAsync(message.DialogId, msg);
        }

        public override async Task OnConnectedAsync()
        {
            onlineUsers.Add(Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            onlineUsers.Remove(Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
            await base.OnDisconnectedAsync(exception);
        }
    }
}
