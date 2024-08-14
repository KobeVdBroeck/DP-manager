using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_manager
{
    internal class ArchiveController : ResourceController<ArchiveResponse, ArchiveEntry>
    {
        private static readonly string STARTQUERY = "query { archive { result { id, worker, week, lab, location, recipients, ppr, category, phase, health, history, reason, remarks }, currentPage, pageCount, totalCount } }";

        public ArchiveController() : base()
        {
            getQueryBuilder = new QueryBuilder(STARTQUERY, "archive");
            SetSort("Id", "asc");
        }
    }
}
