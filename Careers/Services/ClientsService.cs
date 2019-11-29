﻿using System.Threading.Tasks;
using Careers.EF;
using Careers.Models;
using Careers.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Careers.Services
{
    public class ClientsService : IClientsService
    {
        private readonly CareersDbContext context;

        public ClientsService(CareersDbContext context)
        {
            this.context = context;
        }

        public async Task<Client> InsertAsync(Client client)
        {
            client.Id = 0;
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
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Client> FindAsync(int id, bool withOrders = false)
        {
            if (!withOrders) return await context.Clients
                .FirstOrDefaultAsync(x => x.Id == id);
            return await context.Clients
                .Include(x => x.Orders)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
