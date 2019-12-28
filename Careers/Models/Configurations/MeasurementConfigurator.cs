using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Careers.Models.Configurations
{
    public class MeasurementConfigurator : IEntityTypeConfiguration<Measurement>
    {
        public void Configure(EntityTypeBuilder<Measurement> builder)
        {
            builder
              .Property(b => b.TextAZ)
              .IsRequired();

            builder
                .Property(b => b.TextRU)
                .IsRequired();
        }
    }
}
