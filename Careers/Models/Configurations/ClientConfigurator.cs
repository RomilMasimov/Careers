using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Careers.Models.Configurations
{
    public class ClientConfigurator : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder
                .HasIndex(x => x.AppUserId)
                .IsUnique();

            builder
                .Property(x => x.ImageUrl)
                .HasDefaultValue("");

            builder
             .Property(x => x.EmailNotifications)
             .HasDefaultValue(true);

            builder
                .Property(x => x.SmsNotifications)
                .HasDefaultValue(false);
        }
    }
}
