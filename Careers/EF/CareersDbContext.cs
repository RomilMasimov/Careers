﻿using Careers.Models;
using Careers.Models.Enums;
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
            modelBuilder.Entity<Order>()
                .HasOne(x => x.Service)
                .WithMany(x => x.Orders)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Client>()
                .HasIndex(x => x.AppUserId)
                .IsUnique();

            modelBuilder.Entity<Client>()
                .Property(x => x.ImageUrl)
                .HasDefaultValue("");


            modelBuilder.Entity<Specialist>()
                .HasIndex(x => x.AppUserId)
                .IsUnique();

            modelBuilder.Entity<Specialist>()
                .Property(x => x.ImageUrl)
                .HasDefaultValue("");

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
                .HasOne(cg => cg.Specialist)
                .WithMany(cgl => cgl.WhereCanGoList)
                .HasForeignKey(pt => pt.SpecialistId);

            modelBuilder.Entity<MeetingPoint>()
                .HasOne(x => x.City)
                .WithMany(x => x.MeetingPoints)
                .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Answer>()
            //    .HasOne(p => p.AskedQuestion)
            //    .WithMany(b => b.Answers)
            //    .HasForeignKey(p => p.AskedQuestionId);

            //modelBuilder.Entity<Answer>()
            //    .HasOne(p => p.NextQuestion)
            //    .WithMany(b => b.FromAnswers)
            //    .HasForeignKey(p => p.NextQuestionId);

            modelBuilder.Entity<OrderMeetingPoint>()
               .HasOne(pt => pt.MeetingPoint)
               .WithMany(p => p.OrderMeetingPoints)
               .HasForeignKey(pt => pt.MeetingPointId);

            modelBuilder.Entity<OrderMeetingPoint>()
                .HasOne(s => s.Order)
                .WithMany(cml => cml.OrderMeetingPoints)
                .HasForeignKey(si => si.OrderId);

            modelBuilder.Entity<MeetingPoint>()
                .Property(o => o.MeetingPointType)
                .HasConversion<string>();

            modelBuilder.Entity<SpecialistService>()
                .HasOne(pt => pt.Specialist)
                .WithMany(p => p.SpecialistServices)
                .HasForeignKey(pt => pt.SpecialistId);

            modelBuilder.Entity<SpecialistService>()
                .HasOne(s => s.Service)
                .WithMany(cml => cml.SpecialistServices)
                .HasForeignKey(si => si.ServiceId);

            modelBuilder.Entity<SpecialistSubCategory>()
                .HasOne(pt => pt.Specialist)
                .WithMany(p => p.SpecialistSubCategories)
                .HasForeignKey(pt => pt.SpecialistId);

            modelBuilder.Entity<SpecialistSubCategory>()
                .HasOne(s => s.SubCategory)
                .WithMany(cml => cml.SpecialistSubCategories)
                .HasForeignKey(si => si.SubCategoryId);

            modelBuilder.Entity<Question>()
                .Property(x => x.Type).HasConversion<string>()
                .HasDefaultValue(QuestionTypeEnum.Single);

            modelBuilder.Entity<AnswerOrder>()
                .HasOne(pt => pt.Answer)
                .WithMany(p => p.AnswerOrders)
                .HasForeignKey(pt => pt.AnswerId);

            modelBuilder.Entity<AnswerOrder>()
                .HasOne(s => s.Order)
                .WithMany(cml => cml.AnswerOrders)
                .HasForeignKey(si => si.OrderId);

            modelBuilder.Entity<Question>()
                .HasOne(x => x.SubCategory)
                .WithMany(x => x.Questions)
                .OnDelete(DeleteBehavior.NoAction);

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

            modelBuilder.Entity<Client>()
                .Property(x => x.EmailNotifications)
                .HasDefaultValue(true);

            modelBuilder.Entity<Client>()
                .Property(x => x.SmsNotifications)
                .HasDefaultValue(false);

            modelBuilder.Entity<Specialist>()
                .Property(x => x.SmsNotifications)
                .HasDefaultValue(false);

            modelBuilder.Entity<Specialist>()
                .Property(x => x.EmailNotifications)
                .HasDefaultValue(true);

            modelBuilder.Entity<Specialist>()
                .Property(x => x.TakeOrders)
                .HasDefaultValue(true);

            modelBuilder.Entity<Measurement>()
                .Property(b => b.TextAZ)
                .IsRequired();

            modelBuilder.Entity<Measurement>()
                .Property(b => b.TextRU)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .Property(o => o.State)
                .HasConversion<string>();

            modelBuilder.Entity<OrderResponse>()
                .HasOne(pt => pt.Order)
                .WithMany(b => b.OrderResponses)
                .HasForeignKey(pt => pt.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderResponse>()
                .HasOne(pt => pt.Specialist)
                .WithMany(b => b.OrderResponses)
                .HasForeignKey(pt => pt.SpecialistId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserSpecialistMessage>()
                .HasOne(x => x.Order)
                .WithMany(b => b.UserSpecialistMessages)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }

        //public DbSet<OrderSpecialist> OrderSpecialists { get; set; }
        public DbSet<AnswerOrder> AnswerOrders { get; set; }
        //public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
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
        //public DbSet<DefaultQuestion> DefaultQuestions { get; set; }
        public DbSet<OrderSchedule> OrderSchedules { get; set; }
        //public DbSet<SpecialistAnswer> SpecialistAnswers { get; set; }
        public DbSet<UserSpecialistMessage> UserSpecialistMessages { get; set; }
        public DbSet<LanguageSpecialist> LanguageSpecialists { get; set; }
        public DbSet<MyLanguage> Languages { get; set; }
        public DbSet<SpecialistWork> SpecialistWorks { get; set; }
        public DbSet<ReviewComment> ReviewComments { get; set; }
        public DbSet<ServiceReview> ServiceReviews { get; set; }
        public DbSet<OrderResponse> OrderResponses { get; set; }
        public DbSet<Measurement> Measurements { get; set; }

    }
}
