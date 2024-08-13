using DP_manager_API.Entities;

namespace DP_manager_API.Adapters
{
    public static class ArchiveEntryAdapter
    {
        public static ArchiveEntry Adapt(this StockEntry stock, string reason = "No reason specified")
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
                Reason = reason
            };
        }
    }
}
