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
        private readonly CareersDbContext context;

        public MessageService(CareersDbContext context)
        {
            this.context = context;
        }

        public async Task WriteDialogAsync(Dialog dialog)
        {
            var result = await context.UserSpecialistMessages.FirstOrDefaultAsync(x => x.Id == dialog.UserSpecialistMessage.Id);
            if (result != null)
            {
                await using Stream fs = File.OpenWrite(result.LogFilePath);
                await JsonSerializer.SerializeAsync(fs, dialog);
            }
            else
            {
                string fileName = dialog.UserSpecialistMessage.OrderId.ToString() +
                                  dialog.UserSpecialistMessage.ClientId.ToString() +
                                  dialog.UserSpecialistMessage.SpecialistId.ToString()+".json";

                string path = Environment.CurrentDirectory + @"\MessagesLog\" +fileName;

                dialog.UserSpecialistMessage.Id = 0;
                dialog.UserSpecialistMessage.LogFilePath = path;

                await context.UserSpecialistMessages.AddAsync(dialog.UserSpecialistMessage);
                await context.SaveChangesAsync();
            }

            //second variant

            //dialog.UserSpecialistMessage.Id = 0;
            //await context.UserSpecialistMessages.AddAsync(dialog.UserSpecialistMessage);
            //await context.SaveChangesAsync();
            //var fileName = dialog.UserSpecialistMessage.Id + ".json";
            //var path = Environment.CurrentDirectory + @"\MessagesLog\" + fileName;
            //var userSpeChat = await context.UserSpecialistMessages.FirstOrDefaultAsync(x => x.Id == dialog.UserSpecialistMessage.Id);
            //userSpeChat.LogFilePath = path;
            //context.UserSpecialistMessages.Update(userSpeChat);
            //await context.SaveChangesAsync();

        }

        private async Task<Dialog> dialogBodyAsync(UserSpecialistMessage result)
        {
            if (result == null) return null;

            Dialog dialog;
            await using (Stream fs = File.OpenRead(result.LogFilePath))
            {
                dialog = await JsonSerializer.DeserializeAsync<Dialog>(fs);
            }

            if (!dialog.Messages.Any()) return null;
            return dialog;
        }

        public async Task<Dialog> GetDialogAsync(int messageLogId)
        {
            var result = await context.UserSpecialistMessages
                    .FirstOrDefaultAsync(x => x.Id == messageLogId);

            return await dialogBodyAsync(result);
        }

        public async Task<Dialog> GetDialogAsync(int clientId, int specialistId, int orderId)
        {
            var result = await context.UserSpecialistMessages
                .FirstOrDefaultAsync(x => x.OrderId == orderId
                                          && x.ClientId == clientId
                                          && x.SpecialistId == specialistId);

            return await dialogBodyAsync(result);
        }

        public async Task<IEnumerable<Dialog>> GetDialogListAsync(int clientId, int specialistId)
        {
            var result = await context.UserSpecialistMessages
                .FirstOrDefaultAsync(x => x.ClientId == clientId
                                          && x.SpecialistId == specialistId);

            if (result == null) return null;

            IEnumerable<Dialog> dialogs;
            await using (Stream fs = File.OpenRead(result.LogFilePath))
            {
                dialogs = await JsonSerializer.DeserializeAsync<IEnumerable<Dialog>>(fs);
            }

            if (!dialogs.Any()) return null;
            return dialogs;
        }
    }
}

