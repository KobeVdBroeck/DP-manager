using DP_manager_API.Entities;

namespace DP_manager_API.Adapters
{
    public static class StockEntryAdapter
    {
        public static StockEntry WithoutId(this StockEntry stock, string history)
        {
            return new StockEntry()
            {
                Category = stock.Category,
                Health = stock.Health,
                History = history,
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
                Worker = stock.Worker
            };
        }
    }
}
