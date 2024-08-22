using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DP_manager
{
    public abstract class ResourceController<TResponse, TEntity>
    {
        protected QueryBuilder getQueryBuilder;
        protected string insertQueryBuilder;
        protected string updateQueryBuilder;
        protected List<MenuItem> menuItems;
        public List<MenuItem> MenuItems { get => menuItems; }

        public (string field, string value) filter = ("", "");

        public ResourceController()
        {
            menuItems = new List<MenuItem>();
        }

        public async Task<TResponse> GetEntries()
        {
            return await GraphQlService.SendRequestAsync<TResponse>(getQueryBuilder.BuildQuery());
        }

        public async Task<TEntity> InsertEntry(TEntity entity)
        {
            return await GraphQlService.SendRequestAsync<TEntity>(string.Format(UpdateQueryFormat(FormatEntityString(entity)), entity));
        }

        public void RemovePaging()
        {
            getQueryBuilder.RemoveArgument("page");
            getQueryBuilder.RemoveArgument("limit");
        }

        public void RemoveSort()
        {
            getQueryBuilder.RemoveArgument("sortModel");
        }

        public void SetPaging(int page, int limit)
        {
            getQueryBuilder.AddArgument("page", page.ToString(), true);
            getQueryBuilder.AddArgument("limit", limit.ToString(), true);
        }

        public void SetSort(string column, string direction)
        {
            getQueryBuilder.AddArgument("sortModel", "{ fieldName: \"" + column + "\", direction: \"" + direction + "\" }", true);
        }

        public void SetFilter(string column, string filter)
        {
            this.filter = (column, filter);
            getQueryBuilder.AddArgument("filterModel", "{ fieldName: \"" + column + "\", filter: \"" + filter + "\" }", true);
        }

        public void RemoveFilter()
        {
            this.filter = ("", "");
            getQueryBuilder.RemoveArgument("filterModel");
        }

        public string UpdateQueryFormat(string query)
        {
            return query.Replace(" { ", " {{ ").Replace(" } ", " }} ");
        }

        public string FormatStockList(IEnumerable<TEntity> entries)
        {
            return $"[{{{String.Join("},{", entries.Select(e => FormatEntityString(e)))}}}]";
        }

        public string FormatEntityString(TEntity entry)
        {
            return String.Join(", ", entry.GetType().GetProperties().Select(p =>
            {
                var val = p.GetValue(entry);

                if (val is string)
                    val = $"\"{val}\"";
                else if (val != null)
                    val = val.ToString();
                else
                    val = "\"\"";

                return p.Name.Replace(p.Name[0], Char.ToLowerInvariant(p.Name[0])) + ": " + val;
            }));
        }
    }
}