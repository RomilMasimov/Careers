using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Careers.EF;
using Careers.Models;
using Careers.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Careers.Services
{
    public class MessageService : IMessageService
    {
        private readonly CareersDbContext _context;

        public MessageService(CareersDbContext context)
        {
            _context = context;
        }

        public async Task WriteDialogAsync(UserSpecialistMessage uSMessage, Message message)
        {
            if (uSMessage.Id != 0)
            {
                if (uSMessage.LogFilePath != null)
                {
                    await using FileStream fs = new FileStream(uSMessage.LogFilePath, FileMode.Append, FileAccess.Write);
                    await using StreamWriter sw = new StreamWriter(fs);
                    await sw.WriteLineAsync(JsonSerializer.Serialize(message));
                    return;
                }
            }

            {
                var path = Environment.CurrentDirectory + @"\MessagesLog\" + $"{new Guid()}.txt";
                uSMessage.LogFilePath = path;
                await _context.UserSpecialistMessages.AddAsync(uSMessage);
                await _context.SaveChangesAsync();

                await using FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
                await using StreamWriter sw = new StreamWriter(fs);
                await sw.WriteLineAsync(JsonSerializer.Serialize(message));
            }
        }

        private async Task<IEnumerable<Message>> messageBodyAsync(UserSpecialistMessage result)
        {
            if (result == null) return null;

            string jsonStrings;
            await using (FileStream fs = new FileStream(result.LogFilePath, FileMode.Open, FileAccess.Read))
            {
                using var sw = new StreamReader(fs);
                jsonStrings = await sw.ReadToEndAsync();
            }

            var splitedJsonStrings = jsonStrings.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var messages = splitedJsonStrings.Select(item => JsonSerializer.Deserialize<Message>(item)).ToList();

            if (!messages.Any()) return null;
            return messages;
        }

        public async Task<IEnumerable<Message>> GetMessagesAsync(int messageLogId)
        {
            var result = await _context.UserSpecialistMessages
                    .FirstOrDefaultAsync(x => x.Id == messageLogId);

            return await messageBodyAsync(result);
        }

        public async Task<IEnumerable<Message>> GetMessagesAsync(int clientId, int specialistId, int orderId)
        {
            var result = await _context.UserSpecialistMessages
                .FirstOrDefaultAsync(x => x.OrderId == orderId
                                          && x.ClientId == clientId
                                          && x.SpecialistId == specialistId);

            return await messageBodyAsync(result);
        }

        public async Task<IEnumerable<Dialog>> GetDialogListAsync(int clientId, int specialistId)
        {
            var result = await _context.UserSpecialistMessages.Where(x => x.ClientId == clientId && x.SpecialistId == specialistId).ToListAsync();

            if (result == null) return null;

            var dialogs = new List<Dialog>();
            foreach (var item in result)
            {
                dialogs.Add(new Dialog
                {
                    UserSpecialistMessage = item,
                    Messages = await GetMessagesAsync(item.Id)
                });
            }
          
            return !dialogs.Any() ? null : dialogs;
        }
    }
}

