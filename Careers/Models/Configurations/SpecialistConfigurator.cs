using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Careers.Models.Configurations
{
    public class SpecialistConfigurator : IEntityTypeConfiguration<Specialist>
    {
        public void Configure(EntityTypeBuilder<Specialist> builder)
        {
            builder
              .HasIndex(x => x.AppUserId)
              .IsUnique();

            builder
                .Property(x => x.ImageUrl)
                .HasDefaultValue("");

            builder
              .Property(x => x.SmsNotifications)
              .HasDefaultValue(false);

            builder
                .Property(x => x.EmailNotifications)
                .HasDefaultValue(true);

            builder
                .Property(x => x.TakeOrders)
                .HasDefaultValue(true);
        }
    }
}
