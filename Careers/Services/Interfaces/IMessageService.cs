﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Careers.Models;

namespace Careers.Services.Interfaces
{
    public interface IMessageService
    {
        Task WriteDialogAsync(UserSpecialistMessage uSMessage, Message message = null);
        Task<IEnumerable<Message>> GetMessagesAsync(int messageLogId);
        Task<IEnumerable<Message>> GetMessagesAsync(int clientId, int specialistId, int orderId);
        Task<IEnumerable<Dialog>> GetDialogListAsync(int clientId, int specialistId);
        Task<Dialog> GetDialogAsync(int messageLogId);
        Task WriteDialogAsync(int usMessageId, Message message);
    }
}
