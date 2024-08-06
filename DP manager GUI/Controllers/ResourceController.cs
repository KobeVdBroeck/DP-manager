using System.Threading.Tasks;

namespace DP_manager
{
    public abstract class ResourceController<TResponse>
    {
        
        protected QueryBuilder queryBuilder;

        public ResourceController()
        {

        }

        public async Task<TResponse> GetEntries()
        {
            return await GrpcService.GetRequestAsync<TResponse>(queryBuilder.BuildQuery());
        }

        public void RemovePaging()
        {
            queryBuilder.RemoveArgument("page");
            queryBuilder.RemoveArgument("limit");
        }

        public void RemoveSort()
        {
            queryBuilder.RemoveArgument("sortModel");
        }

        public void SetPaging(int page, int limit)
        {
            queryBuilder.AddArgument("page", page.ToString(), true);
            queryBuilder.AddArgument("limit", limit.ToString(), true);
        }

        public void SetSort(string column, string direction)
        {
            queryBuilder.AddArgument("sortModel", "{ fieldName: \"" + column + "\", direction: \"" + direction + "\" }", true);
        }
    }
}