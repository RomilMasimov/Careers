using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Careers.Models.Configurations
{
    public class WhereCanMeetSpecialistConfigurator : IEntityTypeConfiguration<WhereCanMeetSpecialist>
    {
        public void Configure(EntityTypeBuilder<WhereCanMeetSpecialist> builder)
        {
            builder
            .HasOne(pt => pt.WhereCanMeet)
            .WithMany(p => p.WhereCanMeetList)
            .HasForeignKey(pt => pt.WhereCanMeetId);

            builder
                .HasOne(s => s.Specialist)
                .WithMany(cml => cml.WhereCanMeetList)
                .HasForeignKey(si => si.SpecialistId);
        }
    }
}
