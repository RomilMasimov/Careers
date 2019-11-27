using Careers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Services.SpecialistsService
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
