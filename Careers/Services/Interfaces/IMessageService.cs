using System.Collections.Generic;
using System.Threading.Tasks;
using Careers.Models;

namespace Careers.Services.Interfaces
{
    public interface IMessageService
    {
        Task<int> WriteDialogAsync(UserSpecialistMessage uSMessage, Message message = null);
        Task<IEnumerable<Message>> GetMessagesAsync(int messageLogId);
        Task<IEnumerable<Message>> GetMessagesAsync(int clientId, int specialistId, int orderId);
        Task<IEnumerable<Dialog>> GetDialogListAsync(int clientId, int specialistId);
        Task<Dialog> GetDialogAsync(int messageLogId);
        Task<Dialog> GetDialogAsync(int specialistId, int orderId);
        Task WriteDialogAsync(int usMessageId, Message message);
        Task<bool> MarkAsRead(int id, int userId);
    }
}
