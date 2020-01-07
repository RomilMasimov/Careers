using System.Collections.Generic;
using System.Threading.Tasks;
using Careers.Models;

namespace Careers.Services.Interfaces
{
   public interface IAnswerService
    {
        Task<IEnumerable<ClientAnswer>> GetAllInputAnswers(int orderId);
        Task<IEnumerable<ClientAnswer>> GetAllInputAnswers();
        Task<ClientAnswer> GetInputAnswer(int id);
        Task<ClientAnswer> InsertInputAnswer(ClientAnswer answer);
        Task<ClientAnswer> UpdateInputAnswer(ClientAnswer answer);
        Task<bool> DeleteInputAnswer(ClientAnswer answer);
        Task<bool> AddAnswersToOrders(int[] answerIds, int orderId);
        Task<AnswerOrder> AddAnswerToOrder(int answerId, int orderId);
        Task<bool> AddInputAnswersToOrders(IEnumerable<ClientAnswer> answers);
        Task<ClientAnswer> AddInputAnswerToOrder(ClientAnswer answer);
    }
}
