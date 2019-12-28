using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Careers.Models.Configurations
{
    public class OrderResponseConfigurator : IEntityTypeConfiguration<OrderResponse>
    {
        public void Configure(EntityTypeBuilder<OrderResponse> builder)
        {
            builder
                .HasOne(pt => pt.Order)
                .WithMany(b => b.OrderResponses)
                .HasForeignKey(pt => pt.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(pt => pt.Specialist)
                .WithMany(b => b.OrderResponses)
                .HasForeignKey(pt => pt.SpecialistId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
