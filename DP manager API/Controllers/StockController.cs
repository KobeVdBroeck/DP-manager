using DP_manager_API.Adapters;
using DP_manager_API.Data;
using DP_manager_API.Entities;
using DP_manager_API.Models;
using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Controllers;
using GraphQL.AspNet.Interfaces.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace DP_manager_API.Controllers;


public class StockController(AppDbContext dbContext) : GraphController
{
    [QueryRoot("stock")]
    public Entities.PagedResult<StockEntry> RetrieveStockList(FilterModel<StockEntry> filterModel, SortModel<StockEntry> sortModel, int limit = 100, int page = 1)
    {
        var result = dbContext.StockEntries.Include(s => s.Plant).Include(s => s.Medium).AsQueryable();

        if (sortModel != null)
            result = result.OrderBy(sortModel.FieldName + " " + sortModel.Direction);

        if (filterModel != null)
            return new Entities.PagedResult<StockEntry>(result.Where(filterModel.BuildFilterFunction()), page, limit);

        return new Entities.PagedResult<StockEntry>(result, page, limit);
    }

    [MutationRoot("updateStock")]
    public StockEntry UpdateStockEntry(StockEntry stock, string? reason)
    {
        // TODO response with both archived and new entry
        var toUpdate = dbContext.StockEntries.Where(s => s.Id == stock.Id).First();

        if (toUpdate == null)
            throw new Exception("Stock entry not found.");

        dbContext.StockEntries.Remove(toUpdate);

        var archive = toUpdate.Adapt(reason);
        dbContext.ArchiveEntries.Add(archive);
        dbContext.StockEntries.Add(stock.WithoutId(archive.History));

        dbContext.SaveChanges();

        return dbContext.StockEntries.OrderBy("Id").Last();
    }

    [MutationRoot("removeStock")]
    public ArchiveEntry RemoveStockById(int id, string? reason)
    {
        var toUpdate = dbContext.StockEntries.Where(s => s.Id == id).First();

        if (toUpdate == null)
            throw new Exception("Stock entry not found.");

        dbContext.StockEntries.Remove(toUpdate);

        var archive = toUpdate.Adapt(reason);
        dbContext.ArchiveEntries.Add(archive);

        dbContext.SaveChanges();

        return dbContext.ArchiveEntries.OrderBy("Id").Last();
    }

    [QueryRoot("archive")]
    public IEnumerable<ArchiveEntry> RetrieveArchiveList(FilterModel<ArchiveEntry> filterModel, SortModel<ArchiveEntry> sortModel, int limit = 100, int page = 1)
    {
        var result = dbContext.ArchiveEntries.Include(s => s.Plant).Include(s => s.Medium).AsQueryable();

        if (sortModel != null)
            result = result.OrderBy(sortModel.FieldName + " " + sortModel.Direction);

        if (filterModel != null)
            return result.Where(filterModel.BuildFilterFunction());

        return result.Skip(limit * (page - 1)).Take(limit);
    }
}
