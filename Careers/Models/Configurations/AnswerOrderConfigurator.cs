using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Careers.Models.Configurations
{
    public class AnswerOrderConfigurator : IEntityTypeConfiguration<AnswerOrder>
    {
        public void Configure(EntityTypeBuilder<AnswerOrder> builder)
        {
            builder
              .HasOne(pt => pt.Answer)
              .WithMany(p => p.AnswerOrders)
              .HasForeignKey(pt => pt.AnswerId);

            builder
                .HasOne(s => s.Order)
                .WithMany(cml => cml.AnswerOrders)
                .HasForeignKey(si => si.OrderId);
        }
    }
}
