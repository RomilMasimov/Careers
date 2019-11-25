using Careers.Models;
using Microsoft.EntityFrameworkCore;

namespace Careers.EF
{
    public class CareersDbContext : DbContext
    {
        public CareersDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WhereCanMeetSpecialist>()
                .HasOne(pt => pt.WhereCanMeet)
                .WithMany(p => p.WhereCanMeetList)
                .HasForeignKey(pt => pt.WhereCanMeetId);

            modelBuilder.Entity<WhereCanMeetSpecialist>()
                .HasOne(s => s.Specialist)
                .WithMany(cml => cml.WhereCanMeetList)
                .HasForeignKey(si => si.SpecialistId);

            modelBuilder.Entity<WhereCanGoSpecialist>()
                .HasOne(cg => cg.WhereCanGo)
                .WithMany(cgl => cgl.WhereCanGoList)
                .HasForeignKey(pt => pt.WhereCanGoId);

            modelBuilder.Entity<WhereCanGoSpecialist>()
                .HasOne(cg => cg.WhereCanGo)
                .WithMany(cgl => cgl.WhereCanGoList)
                .HasForeignKey(si => si.SpecialistId);

            modelBuilder.Entity<Answer>()
                .HasOne(p => p.AskedQuestion)
                .WithMany(b => b.Answers)
                .HasForeignKey(p => p.AskedQuestionId);

            modelBuilder.Entity<Answer>()
                .HasOne(p => p.NextQuestion)
                .WithMany(b => b.FromAnswers)
                .HasForeignKey(p => p.NextQuestionId);

             modelBuilder.Entity<OrderMeetingPoint>()
                .HasOne(pt => pt.MeetingPoint)
                .WithMany(p => p.OrderMeetingPoints)
                .HasForeignKey(pt => pt.MeetingPointId);

            modelBuilder.Entity<OrderMeetingPoint>()
                .HasOne(s => s.Order)
                .WithMany(cml => cml.OrderMeetingPoints)
                .HasForeignKey(si => si.OrderId);

            modelBuilder.Entity<SpecialistService>()
                .HasOne(pt => pt.Specialist)
                .WithMany(p => p.SpecialistServices)
                .HasForeignKey(pt => pt.SpecialistId);

            modelBuilder.Entity<SpecialistService>()
                .HasOne(s => s.Service)
                .WithMany(cml => cml.SpecialistServices)
                .HasForeignKey(si => si.ServiceId);


        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Specialist> Specialists { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ReviewMedia> ReviewMedias { get; set; }
        public DbSet<MeetingPointType> MeetingPointTypes { get; set; }
        public DbSet<MeetingPoint> MeetingPoints { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServicePrice> ServicePrices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<OrderMeetingPoint> OrderMeetingPoints { get; set; }
        public DbSet<WhereCanGoSpecialist> WhereCanGoSpecialists { get; set; }
        public DbSet<WhereCanMeetSpecialist> WhereCanMeetSpecialists { get; set; }
        public DbSet<SpecialistService> SpecialistServices { get; set; }

    }
}
