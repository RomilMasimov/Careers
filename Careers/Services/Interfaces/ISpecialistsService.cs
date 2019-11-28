using System.Collections.Generic;
using System.Threading.Tasks;
using Careers.Models;

namespace Careers.Services.Interfaces
{
    public interface ISpecialistsService
    {
        Task InsertAsync(Specialist specialist);
        Task UpdateAsync(Specialist specialist);
        Task DeleteAsync(Specialist specialist);

        Task<Specialist> FindAsync(int id);
        Task<IEnumerable<Specialist>> FindAllAsync(Order order);
    }
}
