using Careers.Models;
using Microsoft.EntityFrameworkCore;

namespace Careers.EF
{
    public class CareersDbContext: DbContext
    {
        public CareersDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Specialist> Specialists { get; set; }
    }
}
