using Careers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> InsertAsync(Order order);
        Task<Order> UpdateAsync(Order order);
        Task<bool> DeleteAsync(Order order);
        Task<Order> ChangeOrderStatus(Order order, OrderStateTypeEnum orderState);

        Task<Order> FindAsync(int id);
        Task<IEnumerable<Order>> FindAllByClientAsync(int clientId);
        Task<IEnumerable<Order>> FindAllBySpecialistAsync(int specialistId);
    }
}
