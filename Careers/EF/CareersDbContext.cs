using Careers.Models;
using Careers.Models.Configurations;
using Careers.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Careers.EF
{
    public class CareersDbContext : IdentityDbContext<AppUser>
    {

        public CareersDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>(b =>
            {
                b.HasIndex(u => u.PhoneNumber).IsUnique();
            });

            modelBuilder.ApplyConfiguration(new AnswerOrderConfigurator());
            modelBuilder.ApplyConfiguration(new ClientConfigurator());
            modelBuilder.ApplyConfiguration(new MeasurementConfigurator());
            modelBuilder.ApplyConfiguration(new MeetingPointConfigurator());
            modelBuilder.ApplyConfiguration(new OrderConfigurator());
            modelBuilder.ApplyConfiguration(new OrderConfigurator());
            modelBuilder.ApplyConfiguration(new OrderMeetingPointConfigurator());
            modelBuilder.ApplyConfiguration(new OrderResponseConfigurator());
            modelBuilder.ApplyConfiguration(new QuestionConfigurator());
            modelBuilder.ApplyConfiguration(new SpecialistConfigurator());
            modelBuilder.ApplyConfiguration(new SpecialistServiceConfigurator());
            modelBuilder.ApplyConfiguration(new SpecialistSubCategoryConfigurator());
            modelBuilder.ApplyConfiguration(new UserSpecialistMessageConfigurator());
            modelBuilder.ApplyConfiguration(new WhereCanGoSpecialistConfigurator());
            modelBuilder.ApplyConfiguration(new WhereCanMeetSpecialistConfigurator());

            //    .HasOne(p => p.AskedQuestion)
            //    .WithMany(b => b.Answers)
            //    .HasForeignKey(p => p.AskedQuestionId);

            //modelBuilder.Entity<Answer>()
            //    .HasOne(p => p.NextQuestion)
            //    .WithMany(b => b.FromAnswers)
            //    .HasForeignKey(p => p.NextQuestionId);

            //modelBuilder.Entity<QuestionAnswer>()
            //.HasOne(pt => pt.Question)
            //.WithMany(p => p.QuestionAnswers)
            //.OnDelete(DeleteBehavior.NoAction)
            //.HasForeignKey(pt => pt.QuestionId);

            //modelBuilder.Entity<QuestionAnswer>()
            //.HasOne(s => s.Question)
            //.WithMany(cml => cml.QuestionAnswers)
            //.OnDelete(DeleteBehavior.NoAction)
            //.HasForeignKey(si => si.QuestionId);

            //modelBuilder.Entity<SpecialistAnswer>()
            //  .HasOne(pt => pt.Answer)
            //  .WithMany(b => b.SpecialistAnswers)
            //  .HasForeignKey(pt => pt.AnswerId);

            //modelBuilder.Entity<SpecialistAnswer>()
            //    .HasOne(pt => pt.Specialist)
            //    .WithMany(b => b.SpecialistAnswers)
            //    .HasForeignKey(pt => pt.SpecialistId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ClientAnswer> ClientAnswers { get; set; }
        public DbSet<AnswerOrder> AnswerOrders { get; set; }
        public DbSet<Specialist> Specialists { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ReviewMedia> ReviewMedias { get; set; }
        public DbSet<MeetingPoint> MeetingPoints { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<OrderMeetingPoint> OrderMeetingPoints { get; set; }
        public DbSet<WhereCanGoSpecialist> WhereCanGoSpecialists { get; set; }
        public DbSet<WhereCanMeetSpecialist> WhereCanMeetSpecialists { get; set; }
        public DbSet<SpecialistSubCategory> SpecialistSubCategories { get; set; }
        public DbSet<SpecialistService> SpecialistServices { get; set; }
        public DbSet<OrderSchedule> OrderSchedules { get; set; }
        public DbSet<UserSpecialistMessage> UserSpecialistMessages { get; set; }
        public DbSet<LanguageSpecialist> LanguageSpecialists { get; set; }
        public DbSet<MyLanguage> Languages { get; set; }
        public DbSet<SpecialistWork> SpecialistWorks { get; set; }
        public DbSet<ReviewComment> ReviewComments { get; set; }
        //  public DbSet<ServiceReview> ServiceReviews { get; set; }
        public DbSet<OrderResponse> OrderResponses { get; set; }
        public DbSet<Measurement> Measurements { get; set; }

        //public DbSet<DefaultQuestion> DefaultQuestions { get; set; }
        //public DbSet<OrderSpecialist> OrderSpecialists { get; set; }
        //public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        //public DbSet<SpecialistAnswer> SpecialistAnswers { get; set; }
    }
}
