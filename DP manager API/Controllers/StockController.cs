using DP_manager_API.Data;
using DP_manager_API.Entities;
using DP_manager_API.Models;
using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Controllers;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace DP_manager_API.Controllers;


public class StockController(AppDbContext dbContext) : GraphController
{
    [QueryRoot("stock")]
    public IEnumerable<StockEntry> RetrieveStockList(FilterModel<StockEntry> filterModel, SortModel<StockEntry> sortModel, int limit = 100, int page = 1)
    {
        var result = dbContext.StockEntries
            .Include(s => s.Plant).Include(s => s.Medium)
            .Skip(limit * (page - 1)).Take(limit);

        if (sortModel != null)
        {
            if (sortModel.Direction == SortDirection.Ascending)
                result = result.Order(sortModel.BuildSortFunction());
            else
                result = result.OrderDescending(sortModel.BuildSortFunction());
        }

        if (filterModel != null)
            return result.Where(filterModel.BuildFilterFunction());


        return result;
    }

    [QueryRoot("archive")]
    public IEnumerable<StockEntry> RetrieveArchiveList(FilterModel<ArchiveEntry> filterModel, SortModel<ArchiveEntry> sortModel, int limit = 100, int page = 1)
    {
        var result = dbContext.ArchiveEntries
            .Include(s => s.Plant).Include(s => s.Medium)
            .Skip(limit * (page - 1)).Take(limit);

        if (sortModel != null)
        {
            if (sortModel.Direction == SortDirection.Ascending)
                result = result.Order(sortModel.BuildSortFunction());
            else
                result = result.OrderDescending(sortModel.BuildSortFunction());
        }

        if (filterModel != null)
            return result.Where(filterModel.BuildFilterFunction());


        return result;
    }
}
