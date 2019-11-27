using Careers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Services.ClientsService
{
    public interface IClientsService
    {
        Task InsertAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteAsync(Client client);

        Task<Client> FindAsync(int id);
    }
}
