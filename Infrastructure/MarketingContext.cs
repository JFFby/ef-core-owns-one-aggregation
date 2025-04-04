using ef_core_owns_one_aggregation.Core;
using Microsoft.EntityFrameworkCore;

namespace ef_core_owns_one_aggregation.Infrastructure
{
    public sealed class MarketingContext : DbContext
    {
        public MarketingContext(DbContextOptions<MarketingContext> options)
            : base(options)
        {            
        }

        public DbSet<Campaign> Campaigns { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CampaignEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.LogTo(Console.WriteLine);
    }
}
