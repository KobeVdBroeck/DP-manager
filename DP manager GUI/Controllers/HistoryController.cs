using DP_manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_manager.Controllers
{
    public class HistoryController : ResourceController<HistoryResponse, ArchiveEntry>
    {
        private static readonly string GETQUERY = "query { history { result { id, worker, week, lab, location, recipients, ppr, category, phase, health, history, remarks }, currentPage, pageCount, totalCount, pageLimit }  } ";

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
