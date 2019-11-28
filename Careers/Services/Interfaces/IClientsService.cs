using System.Threading.Tasks;
using Careers.Models;

namespace Careers.Services.Interfaces
{
    public interface IClientsService
    {
        Task InsertAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteAsync(Client client);

        Task<Client> FindAsync(int id);
    }
}
