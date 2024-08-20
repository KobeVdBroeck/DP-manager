using DP_manager_API.Entities;

namespace DP_manager_API.Models
{
    public class SplitResponse
    {
        public IEnumerable<StockEntry> New { get; set; }
        public ArchiveEntry Original { get; set; }
    }
}
