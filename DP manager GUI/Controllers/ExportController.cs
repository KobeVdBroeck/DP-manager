using DP_manager.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_manager.Controllers
{
    public static class ExportController
    {
        static string query = "query {{ export(table: {0}, fileName: \"{1}\") }}";

        public static async Task<bool> ExportData(string table, string filename)
        {
            var response = await GraphQlService.SendRequestAsync<ExportResponse>(FormattedQuery(table, filename));

            return response != null;
        }

        static string FormattedQuery(string table, string file)
        {
            return String.Format(query, table, file);
        }
    }
}
