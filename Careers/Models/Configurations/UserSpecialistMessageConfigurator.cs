using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Careers.Models.Configurations
{
    public class UserSpecialistMessageConfigurator : IEntityTypeConfiguration<UserSpecialistMessage>
    {
        public void Configure(EntityTypeBuilder<UserSpecialistMessage> builder)
        {
            builder.HasOne(x => x.Order)
                    .WithMany(b => b.UserSpecialistMessages)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
