using DP_manager.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DP_manager
{
    public class StockController : ResourceController<StockResponse, StockEntry>
    {
        private static readonly string GETQUERY = "query { stock { result { id, worker, week, lab, location, recipients, ppr, category, phase, health, history, remarks, mediumId, plantCode }, currentPage, pageCount, totalCount } }";
        private static readonly string UPDATEQUERY = "mutation { updateStock( stock: { id: {0}, worker: \"{1}\", week: \"{2}\", lab: \"{3}\", location: \"{4}\", recipients: {5}, ppr: {6}, category: {7}, phase: {8}, health: {9}, history: \"{10}\", remarks: \"{11}\", mediumId: {12}, plantCode: \"{13}\", } , reason: \"{14}\") { id, worker, week, lab, location, recipients, ppr, category, phase, health, history, remarks }  } ";
        private static readonly string REMOVEQUERY = "mutation { removeStock( id: {0} } , reason: \"{1}\") { id, worker, week, lab, location, recipients, ppr, category, phase, health, history, remarks, reason }  } ";

        public StockController() : base()
        {
            getQueryBuilder = new QueryBuilder(GETQUERY, "stock");

            menuItems.Add(new FormBoundMenuItem("Update", new UpdateStockForm(this))); 
            menuItems.Add(new FormBoundMenuItem("Remove", new ArchiveStockForm(this)));


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

            return string.Format(UpdateFormat(UPDATEQUERY), vals.Select(v => v.ToString()).ToArray());
        }

        public string UpdateFormat(string query)
        {
            return query.Replace(" { ", " {{ ").Replace(" } ", " }} ");
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
            await GrpcService.SendRequestAsync<StockEntry>(FormatUpdateQuery(response, reason));
        }

        public async Task RemoveEntry(int id, string reason = "No reason specified.")
        {
            await GrpcService.SendRequestAsync<StockEntry>(string.Format(UpdateFormat(REMOVEQUERY), id, reason));
        }
    }
}
