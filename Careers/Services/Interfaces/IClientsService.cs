﻿using System.Threading.Tasks;
using Careers.Models;

namespace Careers.Services.Interfaces
{
    public interface IClientsService
    {
        Task<Client> AddAsync(Client client);
        Task<Client> UpdateAsync(Client client);
        Task<bool> DeleteAsync(Client client);
        Task<Client> FindAsync(int id,bool withOrders=false);

    }
}
