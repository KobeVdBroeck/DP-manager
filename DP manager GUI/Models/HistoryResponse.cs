using System.Collections;
using System.Collections.Generic;

namespace DP_manager.Models
{
    public class HistoryResponse : IGraphQlResponse
    {
        public PagedResponse<IEnumerable<ArchiveEntry>> History { get; set; }

        public IEnumerable GetData()
        {
            return History.Result;
        }

        public (int page, int pageCount) GetPageInfo()
        {
            return (History.CurrentPage, History.PageCount);
        }
    }
}
