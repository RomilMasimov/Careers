using Careers.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.SignalR
{
    public class NotificationHub : Hub
    {
        private readonly ISpecialistService specialistService;

        //public NotificationHub(ISpecialistService specialistService)
        //{
        //    this.specialistService = specialistService;
        //}

        //public async Task OrderCreated(int id)
        //{
        //    var specialists = await specialistService.GetAllAppUserIdsAsync();
        //    await Clients.Users(specialists.ToList()).SendAsync("NewOrder", id);
        //}
    }
}
