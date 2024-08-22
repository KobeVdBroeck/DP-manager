namespace DP_manager
{
    internal class ArchiveController : ResourceController<ArchiveResponse, ArchiveEntry>
    {
        private static readonly string STARTQUERY = "query { getArchive { result { id, worker, week, lab, location, recipients, ppr, category, phase, health, history, remarks, reason, plantCode, mediumId }, currentPage, pageCount, totalCount } }";

        public ArchiveController() : base()
        {
            getQueryBuilder = new QueryBuilder(STARTQUERY, "archive");
            SetSort("Id", "asc");
        }
    }
}
