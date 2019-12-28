using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Careers.Models.Configurations
{
    public class WhereCanGoSpecialistConfigurator : IEntityTypeConfiguration<WhereCanGoSpecialist>
    {
        public void Configure(EntityTypeBuilder<WhereCanGoSpecialist> builder)
        {
            builder
               .HasOne(cg => cg.WhereCanGo)
               .WithMany(cgl => cgl.WhereCanGoList)
               .HasForeignKey(pt => pt.WhereCanGoId);

            builder
                .HasOne(cg => cg.Specialist)
                .WithMany(cgl => cgl.WhereCanGoList)
                .HasForeignKey(pt => pt.SpecialistId);
        }
    }
}
