namespace ef_core_owns_one_aggregation.Core;

public class Campaign
{
    public Campaign(
        string name,
        CampaignStatistics statistics,
        Guid organizationId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Statistics = statistics;
        OrganizationId = organizationId;
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// EF ctor
    /// </summary>
    private Campaign()
    {        
    }

    public Guid Id { get; private set; }

    public string Name { get; private set;}
    
    public CampaignStatistics Statistics { get; private set; }
    
    public Guid OrganizationId { get; private set; }

    public DateTime CreatedAt { get; private set; }
}
