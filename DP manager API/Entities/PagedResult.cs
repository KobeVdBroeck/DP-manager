using System.Collections.Generic;

namespace DP_manager_API.Entities
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Result { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int TotalCount { get; set; }

        public PagedResult(IEnumerable<T> result, int page, int pageLimit)
        {
            Result = result.Skip(pageLimit * (page - 1)).Take(pageLimit);
            TotalCount = result.Count();
            PageCount = 1 + (int) Math.Floor((decimal)(TotalCount / pageLimit));
            CurrentPage = page;
        }
    }
}
