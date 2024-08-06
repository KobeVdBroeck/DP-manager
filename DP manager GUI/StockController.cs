using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_manager
{
    internal class StockController : ResourceController<StockResponse>
    {
        private static readonly string STARTQUERY = "query { stock { result { id, worker, week, lab, location, recipients, ppr, category, phase, health, history, remarks }, currentPage, pageCount, totalCount } }";

        public StockController() 
        {
            queryBuilder = new QueryBuilder(STARTQUERY, "stock");
            SetSort("Id", "asc");
        }
    }
}
