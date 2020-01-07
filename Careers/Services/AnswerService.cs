using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Careers.EF;
using Careers.Models;
using Careers.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Careers.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly CareersDbContext _context;

        public AnswerService(CareersDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClientAnswer>> GetAllInputAnswers(int orderId)
        {
            return await _context.ClientAnswers.Where(x => x.OrderId == orderId).ToListAsync();
        }

        public async Task<IEnumerable<ClientAnswer>> GetAllInputAnswers()
        {
            return await _context.ClientAnswers.ToListAsync();
        }

        public async Task<ClientAnswer> GetInputAnswer(int id)
        {
            return await _context.ClientAnswers.FindAsync(id);
        }

        public async Task<ClientAnswer> InsertInputAnswer(ClientAnswer answer)
        {
            answer.Id = 0;
            var updatedAnswer = await _context.ClientAnswers.AddAsync(answer);
            await _context.SaveChangesAsync();
            return updatedAnswer.Entity;
        }

        public async Task<ClientAnswer> UpdateInputAnswer(ClientAnswer answer)
        {
            var updatedAnswer = _context.ClientAnswers.Update(answer);
            await _context.SaveChangesAsync();
            return updatedAnswer.Entity;
        }

        public async Task<bool> DeleteInputAnswer(ClientAnswer answer)
        {
            _context.ClientAnswers.Remove(answer);
            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> AddAnswersToOrders(int[] answerIds, int orderId)
        {
            await _context.AnswerOrders.AddRangeAsync(answerIds.Select(x => new AnswerOrder
            {
                OrderId = orderId,
                AnswerId = x
            }));

            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<AnswerOrder> AddAnswerToOrder(int answerId,int orderId)
        {
           var result= await _context.AnswerOrders.AddAsync( new AnswerOrder
            {
                OrderId = orderId,
                AnswerId = answerId
            });

             await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> AddInputAnswersToOrders(IEnumerable<ClientAnswer> answers)
        {
           await _context.ClientAnswers.AddRangeAsync(answers);
           var rows = await _context.SaveChangesAsync();
           return rows > 0;
        }

        public async Task<ClientAnswer> AddInputAnswerToOrder(ClientAnswer answer)
        {
           var result= await _context.ClientAnswers.AddAsync(answer);
            return result.Entity;
        }
    }
}
