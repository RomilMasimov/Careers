using System;
using System.Threading.Tasks;
using Careers.EF;
using Careers.Models;
using Careers.Services.Interfaces;

namespace Careers.Services
{
    public class ClientsService : IClientsService
    {
        private readonly CareersDbContext _context;

        public ClientsService(CareersDbContext context)
        {
            _context = context;
        }

        public async Task<Client> AddAsync(Client client)
        {
            var person = await _context.Persons.AddAsync(client.Person);
            client.Person = person.Entity;
            var result = await _context.Clients.AddAsync(client);
            return result.Entity;
        }

        public async Task<Client> UpdateAsync(Client client)
        {
            throw new NotImplementedException();
        }

        public async Task<Client> DeleteAsync(Client client)
        {
            throw new NotImplementedException();
        }

        public async Task<Client> FindAsync(int id, bool withOrders = false)
        {
            throw new NotImplementedException();
        }
    }
}
