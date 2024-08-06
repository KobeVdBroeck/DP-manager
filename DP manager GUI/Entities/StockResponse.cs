using System.Collections;
using System.Collections.Generic;

namespace DP_manager
{
    public class StockResponse : IGrpcResponse
    {
        public PagedResponse<IEnumerable<StockEntry>> Stock { get; set; }

        public IEnumerable GetData()
        {
            return Stock.Result;
        }

        public (int page, int pageCount) GetPageInfo()
        {
            return (Stock.CurrentPage, Stock.PageCount);
        }
    }
}
