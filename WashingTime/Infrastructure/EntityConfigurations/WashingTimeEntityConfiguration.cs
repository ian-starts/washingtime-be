using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WashingTime.Infrastructure.EntityConfigurations
{
    public class WashingTimeEntityConfiguration : IEntityTypeConfiguration<Entities.WashingTime.WashingTime>
    {
        public void Configure(EntityTypeBuilder<Entities.WashingTime.WashingTime> builder)
        {
            builder.HasIndex(o => new { o.StartDateTime, o.EndDateTime, o.WasherType }).IsUnique();
        }
    }
}