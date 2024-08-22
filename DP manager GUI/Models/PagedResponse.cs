namespace DP_manager
{
    public class PagedResponse<T>
    {
        public T Result { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int TotalCount { get; set; }
    }
}
