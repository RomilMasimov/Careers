using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Careers.Models.Configurations
{
    public class SpecialistServiceConfigurator : IEntityTypeConfiguration<SpecialistService>
    {
        public void Configure(EntityTypeBuilder<SpecialistService> builder)
        {
            builder
                .HasOne(pt => pt.Specialist)
                .WithMany(p => p.SpecialistServices)
                .HasForeignKey(pt => pt.SpecialistId);

            builder
                .HasOne(s => s.Service)
                .WithMany(cml => cml.SpecialistServices)
                .HasForeignKey(si => si.ServiceId);
        }
    }
}
