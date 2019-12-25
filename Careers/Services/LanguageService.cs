using System.Collections.Generic;
using System.Threading.Tasks;
using Careers.EF;
using Careers.Models;
using Microsoft.EntityFrameworkCore;

namespace Careers.Services
{
    public class LanguageService 
    {
        private readonly CareersDbContext _context;

        public LanguageService(CareersDbContext context)
        {
            _context = context;
        }

        public async Task<List<MyLanguage>> GetAllAsync()
        {
            return await _context.Languages.ToListAsync();
        }
    }
}
