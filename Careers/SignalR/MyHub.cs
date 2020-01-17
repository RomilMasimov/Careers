using System;
using System.Threading.Tasks;
using Careers.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Careers.SignalR
{
    [Authorize]
    public class MyHub : Hub
    {
        private readonly UserManager<AppUser> _userManager;

        public MyHub(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Send(string userEmail, string message)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            await this.Clients.User(user.Id).SendAsync("ReceiveMessage", message);
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
