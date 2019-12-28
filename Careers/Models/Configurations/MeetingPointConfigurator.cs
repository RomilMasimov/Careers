using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Careers.Models.Configurations
{
    public class MeetingPointConfigurator : IEntityTypeConfiguration<MeetingPoint>
    {
        public void Configure(EntityTypeBuilder<MeetingPoint> builder)
        {
            builder
               .HasOne(x => x.City)
               .WithMany(x => x.MeetingPoints)
               .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(o => o.MeetingPointType)
                .HasConversion<string>();
        }
    }
}
