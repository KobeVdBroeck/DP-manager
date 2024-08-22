using DP_manager.Models;

namespace DP_manager.Controllers
{
    public class HistoryController : ResourceController<HistoryResponse, ArchiveEntry>
    {
        private static readonly string GETQUERY = "query { getHistory { result { id, worker, week, lab, location, recipients, ppr, category, phase, health, history, remarks }, currentPage, pageCount, totalCount, pageLimit }  } ";

        string history;
        public string History
        {
            get => history;
            set
            {
                history = value;
                getQueryBuilder.AddArgument("history", "\"" + value + "\"", true);
                var a = getQueryBuilder.BuildQuery();
                var b = 1;
            }
        }

        public HistoryController() : base()
        {
            getQueryBuilder = new QueryBuilder(GETQUERY, "history");
        }
    }
}
