using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_manager.Models
{
    public class HistoryResponse : IGrpcResponse
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
