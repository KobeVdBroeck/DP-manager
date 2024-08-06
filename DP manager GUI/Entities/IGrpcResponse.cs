using System.Collections;

namespace DP_manager
{
    public interface IGrpcResponse
    {
        IEnumerable GetData();
        (int page, int pageCount) GetPageInfo();
    }
}