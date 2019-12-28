using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Careers.Models.Configurations
{
    public class OrderConfigurator : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
               .HasOne(x => x.Service)
               .WithMany(x => x.Orders)
               .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(o => o.State)
                .HasConversion<string>();
        }
    }
}
