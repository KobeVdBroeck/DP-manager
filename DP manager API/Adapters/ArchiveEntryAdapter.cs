using DP_manager_API.Entities;

namespace DP_manager_API.Adapters;

public static class ArchiveEntryAdapter
{
    private static readonly string DEFAULT_REASON = "No reason specified";

    public static ArchiveEntry Adapt(this StockEntry stock, string? reason)
    {
        return new ArchiveEntry()
        {
            Category = stock.Category,
            Health = stock.Health,
            History = stock.History + stock.Id + ";",
            Lab = stock.Lab,
            Location = stock.Location,
            Medium = stock.Medium,
            MediumId = stock.MediumId,
            Phase = stock.Phase,
            Plant = stock.Plant,
            PlantCode = stock.PlantCode,
            Ppr = stock.Ppr,
            Recipients = stock.Recipients,
            Remarks = stock.Remarks,
            Week = stock.Week,
            Worker = stock.Worker,
            Reason = reason ?? DEFAULT_REASON,
        };
    }
}
