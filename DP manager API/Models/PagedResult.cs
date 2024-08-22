namespace DP_manager_API.Models;

public class PagedResult<T>
{
    public IEnumerable<T> Result { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public int TotalCount { get; set; }
    public int PageLimit { get; set; }

    public PagedResult(IEnumerable<T> result, int page, int pageLimit, int totalCount)
    {
        Result = result.Skip(pageLimit * (page - 1)).Take(pageLimit);
        TotalCount = totalCount;
        PageCount = 1 + (int)Math.Floor((decimal)(TotalCount / pageLimit));
        CurrentPage = page;
        PageLimit = pageLimit;
    }
}
