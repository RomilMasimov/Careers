using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Careers.Models.Configurations
{
    public class SpecialistSubCategoryConfigurator : IEntityTypeConfiguration<SpecialistSubCategory>
    {
        public void Configure(EntityTypeBuilder<SpecialistSubCategory> builder)
        {
            builder
              .HasOne(pt => pt.Specialist)
              .WithMany(p => p.SpecialistSubCategories)
              .HasForeignKey(pt => pt.SpecialistId);

            builder
                .HasOne(s => s.SubCategory)
                .WithMany(cml => cml.SpecialistSubCategories)
                .HasForeignKey(si => si.SubCategoryId);
        }
    }
}
