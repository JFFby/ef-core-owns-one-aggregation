namespace ef_core_owns_one_aggregation.Core;

public sealed class CampaignStatistics
{
    public CampaignStatistics(
        int orders,
        int revenue,
        int views)
    {
        Orders = orders;
        Revenue = revenue;
        Views = views;
    }

    /// <summary>
    /// EF ctor
    /// </summary>
    private CampaignStatistics()
    {        
    }

    public int Orders { get; private set;}
    public int Revenue { get; private set; }
    public int Views { get; private set; }
}
