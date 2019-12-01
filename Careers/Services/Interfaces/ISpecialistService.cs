using System.Collections.Generic;
using System.Threading.Tasks;
using Careers.Models;

namespace Careers.Services.Interfaces
{
    public interface ISpecialistService
    {
        Task<Specialist> InsertAsync(Specialist specialist);
        Task<Specialist> UpdateAsync(Specialist specialist);
        Task<bool> DeleteAsync(Specialist specialist);

        Task<Specialist> FindAsync(int id);
        Task<IEnumerable<Specialist>> FindAllAsync(Order order);
        

       
    }

}
