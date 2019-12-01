using System.Threading.Tasks;
using Careers.Models;

namespace Careers.Services.Interfaces
{
    public interface IClientService
    {
        Task<Client> InsertAsync(Client client);
        Task<Client> UpdateAsync(Client client);
        Task<bool> DeleteAsync(Client client);
        Task<Client> FindAsync(int id, bool withOrders=false);
    }
}
