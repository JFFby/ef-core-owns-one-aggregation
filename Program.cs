using ef_core_owns_one_aggregation.Core;
using ef_core_owns_one_aggregation.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL.Migrations;
using System.Text.Json;

var organizationId = Guid.Empty;
var configuration = new ConfigurationBuilder()
     .AddJsonFile($"appsettings.json")
     .Build();

var connectionString = configuration.GetConnectionString("Marketing");

var services = new ServiceCollection()
    .AddDbContext<MarketingContext>(options =>
    {
        options.UseNpgsql(connectionString)
            .ReplaceService<IMigrationsSqlGenerator, NpgsqlMigrationsSqlGenerator>();
    })
    .BuildServiceProvider();

var context = services.GetRequiredService<MarketingContext>();

MakeSureDbIsNotEmpty(context, organizationId);

var totals = context.Campaigns.Where(x => x.Name.EndsWith("one"))
    .Where(x => x.OrganizationId == organizationId)
    .GroupBy(x => x.OrganizationId)
    .Select(x => new CampaignsTotals
    {
        Orders = x.Sum(c => c.Statistics.Orders),
        Revenue = x.Sum(c => c.Statistics.Revenue),
        Views = x.Sum(c => c.Statistics.Views),
    })
    .First();
Console.WriteLine(
    JsonSerializer.Serialize(totals, new JsonSerializerOptions { WriteIndented = true }));

static void MakeSureDbIsNotEmpty(MarketingContext context, Guid organizationId)
{
    context.Database.EnsureCreated();
    if (!context.Campaigns.Any())
    {
        var campaign1 = new Campaign("the first one", new CampaignStatistics(1, 100, 10), organizationId);
        var campaign2 = new Campaign("the second one", new CampaignStatistics(3, 300, 30), organizationId);
        var campaign3 = new Campaign("failed campaign", new CampaignStatistics(1, 10, 40), organizationId);

        context.AddRange([campaign1, campaign2, campaign3]);

        context.SaveChanges();
    }

}