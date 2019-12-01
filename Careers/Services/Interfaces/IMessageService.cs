using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Careers.Models;

namespace Careers.Services.Interfaces
{
    interface IMessageService
    {
        Task<IEnumerable<Message>> GetChatAsync<T>(int clientId,int specialistId);
        Task WriteAsync<T>(T author);
        Task WriteAllAsync(Dialog dialog);


    }
}
