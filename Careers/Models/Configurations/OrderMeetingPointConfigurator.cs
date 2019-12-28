using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Careers.Models.Configurations
{
    public class OrderMeetingPointConfigurator : IEntityTypeConfiguration<OrderMeetingPoint>
    {
        public void Configure(EntityTypeBuilder<OrderMeetingPoint> builder)
        {
            builder
             .HasOne(pt => pt.MeetingPoint)
             .WithMany(p => p.OrderMeetingPoints)
             .HasForeignKey(pt => pt.MeetingPointId);

            builder
                .HasOne(s => s.Order)
                .WithMany(cml => cml.OrderMeetingPoints)
                .HasForeignKey(si => si.OrderId);
        }
    }
}
