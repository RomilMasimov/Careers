using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Careers.Models;
using Microsoft.AspNetCore.Http;

namespace Careers.Services.Interfaces
{
    public interface ISpecialistService
    {
        Task<Specialist> InsertAsync(Specialist specialist);
        Task<Specialist> UpdateAsync(Specialist specialist);
        Task<Specialist> UpdateAbotAsync(int specialistId, string about);
        Task<bool> DeleteAsync(Specialist specialist);

        Task<bool> UpdatePasport(int specialistId, Stream file);
        Task<bool> UpdateImage(int specialistId, Stream file);
        Task<bool> DeleteImage(int specialistId);

        Task<IEnumerable<SpecialistWork>> FindAllWorks(int specialistId);
        Task<SpecialistWork> FindWork(int specialistWorkId);
        Task<SpecialistWork> AddWork(int specialistId, Stream file, string description);
        Task<SpecialistWork> EditWork(int workId, string description);
        Task<bool> DeleteWork(int id);

        Task<Education> AddEducation(Education education);
        Task<Education> UpdateEducation(Education education);
        Task<bool> DeleteEducation(int id);

        Task<Specialist> FindAsync(int id);
        Task<Specialist> FindByUserAsync(string userId);
        //need to check
        Task<IEnumerable<Specialist>> FindAllAsync(Order order);
    }

}
