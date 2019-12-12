using System;
using System.Threading.Tasks;
using Careers.EF;
using Careers.Models;
using Careers.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Careers.Services
{
    public class ClientService : IClientService
    {
        private readonly CareersDbContext context;

        public ClientService(CareersDbContext context)
        {
            this.context = context;
        }

        public async Task<Client> InsertAsync(Client client)
        {
            client.Id = 0;
            client.LastVisit=DateTime.Now;
            var result = await context.Clients.AddAsync(client);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Client> UpdateAsync(Client client)
        {
            context.Clients.Update(client);
            await context.SaveChangesAsync();
            return client;
        }

        public async Task<bool> DeleteAsync(Client client)
        {
            context.Clients.Remove(client);

            return await context.SaveChangesAsync() > 0;
        }

        public async Task<Client> FindAsync(int id, bool withOrders = false)
        {
            if (!withOrders) return await context.Clients
                .FirstOrDefaultAsync(x => x.Id == id);
            return await context.Clients
                .Include(x => x.Orders)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

         public async Task<Client> FindAsync(string userId, bool withOrders = false)
        {
            if (!withOrders) return await context.Clients
                .FirstOrDefaultAsync(x => x.AppUserId == userId);
            return await context.Clients
                .Include(x => x.Orders)
                .FirstOrDefaultAsync(x => x.AppUserId == userId);
        }
    }
}
