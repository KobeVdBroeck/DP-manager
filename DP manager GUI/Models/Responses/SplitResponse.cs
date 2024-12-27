using DP_manager;
using System.Collections.Generic;

namespace DP_manager_API.Models
{
    public class SplitResponse
    {
        public IEnumerable<StockEntry> New { get; set; }
        public ArchiveEntry Original { get; set; }
    }
}
