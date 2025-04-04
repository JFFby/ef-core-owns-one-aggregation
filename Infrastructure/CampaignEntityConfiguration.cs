using ef_core_owns_one_aggregation.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ef_core_owns_one_aggregation.Infrastructure;

public sealed class CampaignEntityConfiguration : IEntityTypeConfiguration<Campaign>
{
    public void Configure(EntityTypeBuilder<Campaign> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(256);
        builder.Property(x => x.OrganizationId);
        builder.Property(x => x.CreatedAt);
        builder.OwnsOne(x => x.Statistics, x =>
        {
            x.Property(s => s.Orders);
            x.Property(s => s.Revenue);
            x.Property(s => s.Views);
        });
    }
}
