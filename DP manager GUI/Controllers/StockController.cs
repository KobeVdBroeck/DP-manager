using DP_manager.Components;
using DP_manager.Controllers;
using DP_manager_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DP_manager
{
    public class StockController : ResourceController<StockResponse, StockEntry>
    {
        private static readonly string GETQUERY = "query { getStock { result { id, worker, week, lab, location, recipients, ppr, category, phase, health, history, remarks, mediumId, plantCode }, currentPage, pageCount, totalCount } }";
        private static readonly string UPDATEQUERY = "mutation { updateStock( stock: { id: {0}, worker: \"{1}\", week: \"{2}\", lab: \"{3}\", location: \"{4}\", recipients: {5}, ppr: {6}, category: {7}, phase: {8}, health: {9}, history: \"{10}\", remarks: \"{11}\", mediumId: {12}, plantCode: \"{13}\", } , reason: \"{14}\") { id, worker, week, lab, location, recipients, ppr, category, phase, health, history, remarks }  } ";
        private static readonly string REMOVEQUERY = "mutation { removeStock( id: {0} , reason: \"{1}\") { id, worker, week, lab, location, recipients, ppr, category, phase, health, history, remarks, reason }  } ";
        private static readonly string SPLITQUERY = "mutation { splitStock( id: {0}, newEntries: {1}, reason: {2}) { new { id worker week lab location recipients ppr category phase health history remarks medium { id description } mediumId plant { code } plantCode } , original { reason id worker week lab location recipients ppr category phase health history remarks medium { id description } mediumId plant { code } plantCode }  }  } ";
        private static readonly string INSERTQUERY = "mutation { addStock(stock: { {0} } ) { id, worker, week, lab, location, recipients, ppr, category, phase, health, history, remarks }  } ";

        public StockController() : base()
        {
            getQueryBuilder = new QueryBuilder(GETQUERY, "stock");
            insertQueryBuilder = INSERTQUERY;

            menuItems.Add(new FormBoundMenuItem("Update", new AddStockForm(this, true)));
            menuItems.Add(new FormBoundMenuItem("Remove", new ArchiveStockForm(this)));
            menuItems.Add(new FormBoundMenuItem("History", new HistoryForm(new HistoryController())));
            menuItems.Add(new FormBoundMenuItem("Split", new SplitForm(this)));
            menuItems.Add(new FormBoundMenuItem("New entry", new AddStockForm(this, false)));

            SetSort("Id", "asc");
        }


        public string FormatUpdateQuery(StockEntry response, string reason = "No reason specified.")
        {
            var vals = new object[]
            {
                response.Id,
                response.Worker,
                response.Week,
                response.Lab,
                response.Location,
                response.Recipients,
                response.Ppr,
                response.Category,
                response.Phase,
                response.Health,
                response.History,
                response.Remarks,
                1125,
                "0DP",
                reason
            };

            return string.Format(UpdateQueryFormat(UPDATEQUERY), vals.Select(v => v.ToString()).ToArray());
        }

        public async Task UpdateEntries(IEnumerable<StockEntry> response, string reason = "No reason specified.")
        {
            foreach (var entry in response)
            {
                await UpdateEntry(entry, reason);
            }
        }

        public async Task UpdateEntry(StockEntry response, string reason = "No reason specified.")
        {
            await GraphQlService.SendRequestAsync<StockEntry>(FormatUpdateQuery(response, reason));
        }

        public async Task RemoveEntry(int id, string reason = "No reason specified.")
        {
            await GraphQlService.SendRequestAsync<StockEntry>(string.Format(UpdateQueryFormat(REMOVEQUERY), id, reason));
        }

        public async Task SplitEntry(int id, IEnumerable<StockEntry> entries, string reason)
        {
            await GraphQlService.SendRequestAsync<SplitResponse>(string.Format(UpdateQueryFormat(SPLITQUERY), id, FormatStockList(entries), $"\"{reason}\""));
        }
    }
}
