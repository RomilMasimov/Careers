using System.Collections.Generic;
using System.Threading.Tasks;
using Careers.Models;

namespace Careers.Services.Interfaces
{
    public interface IMessageService
    {
        Task WriteDialogAsync(UserSpecialistMessage uSMessage, Message message);
        Task<IEnumerable<Message>> GetMessagesAsync(int messageLogId);
        Task<IEnumerable<Message>> GetMessagesAsync(int clientId, int specialistId, int orderId);
        Task<IEnumerable<Dialog>> GetDialogListAsync(int clientId, int specialistId);
        
    }
}
