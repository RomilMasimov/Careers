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
        Task<Order> ChangeIsActiveOrderAsync(int orderId, bool isActive);

        Task<OrderResponse> InsertResponseAsync(OrderResponse response);
        Task<OrderResponse> UpdateResponseAsync(OrderResponse response);
        Task<bool> DeleteResponseAsync(OrderResponse response);

        Task<IEnumerable<OrderResponse>> FindAllResponseByOrderAsync(int orderId);

        Task<Order> FindAsync(int id, bool responses = false);
        Task<IEnumerable<Order>> FindAllAsync();
        Task<Order> FindDetailedAsync(int id);
        Task<IEnumerable<Order>> FindAllByClientAsync(int clientId);
        Task<IEnumerable<Order>> FindAllBySpecialistAsync(int specialistId);
        Task<IEnumerable<Order>> FindAllForSpecialistAsync(string specialistAppUserId);
        Task<IEnumerable<Order>> FindAllForSpecialistByClientAsync(int specialistId, int clientId);
        Task<bool> AddMeetingPoints(IEnumerable<OrderMeetingPoint> orderMeetingPoints);
        Task<bool> UpdateAnswerOrdersAsync(IEnumerable<int> answers, int orderId);
        Task<IEnumerable<Order>> FindAllResponsesAsync(int specialistId);
    }
}
