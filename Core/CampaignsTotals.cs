namespace ef_core_owns_one_aggregation.Core;

public sealed class CampaignsTotals
{
    public required int Orders { get; init; }
    public required int Revenue { get; init; }
    public required int Views { get; init; }
}
