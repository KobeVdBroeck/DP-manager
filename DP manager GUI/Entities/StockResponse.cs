using System.Collections.Generic;

namespace DP_manager
{
    public class StockResponse
    {
        public PagedResponse<IEnumerable<StockEntry>> Stock { get; set; }
    }
}
