using System.Collections.Generic;
using System.Threading.Tasks;
using Careers.Models;

namespace Careers.Services.Interfaces
{
    interface IMessageService
    {
        Task WriteDialogAsync(Dialog dialog);
        Task<Dialog> GetDialogAsync(int messageLogId);
        Task<Dialog> GetDialogAsync(int clientId, int specialistId, int orderId);
        Task<IEnumerable<Dialog>> GetDialogListAsync(int clientId, int specialistId);


    }
}
