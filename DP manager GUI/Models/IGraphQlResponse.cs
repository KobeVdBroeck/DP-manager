using System.Collections;

namespace DP_manager
{
    public interface IGraphQlResponse
    {
        IEnumerable GetData();
        (int page, int pageCount) GetPageInfo();
    }
}