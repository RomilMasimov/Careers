using Careers.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Careers.Models.Configurations
{
    public class QuestionConfigurator : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder
                .Property(x => x.Type).HasConversion<string>()
                .HasDefaultValue(QuestionTypeEnum.Single);

            builder
              .HasOne(x => x.SubCategory)
              .WithMany(x => x.Questions)
              .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
