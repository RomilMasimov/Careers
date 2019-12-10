using Careers.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Careers.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> InsertAsync(Order order);
        Task<Order> UpdateAsync(Order order);
        Task<bool> DeleteAsync(Order order);

        Task<Order> FindAsync(int id, bool responses = false);
        Task<IEnumerable<Order>> FindAllByClientAsync(int clientId);
        Task<IEnumerable<Order>> FindAllBySpecialistAsync(int specialistId);
    }
}
