using System.Collections;
using System.Collections.Generic;

namespace DP_manager
{
    public class ArchiveResponse : IGraphQlResponse
    {
        public PagedResponse<IEnumerable<ArchiveEntry>> Archive { get; set; }

        public IEnumerable GetData()
        {
            return Archive.Result;
        }

        public (int page, int pageCount) GetPageInfo()
        {
            return (Archive.CurrentPage, Archive.PageCount);
        }
    }
}
