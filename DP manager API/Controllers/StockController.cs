using DP_manager_API.Adapters;
using DP_manager_API.Data;
using DP_manager_API.Entities;
using DP_manager_API.Models;
using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Controllers;
using GraphQL.AspNet.Interfaces.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            return new Entities.PagedResult<StockEntry>(result.Where(filterModel.BuildFilterFunction()), page, limit, result.Count());

        return new Entities.PagedResult<StockEntry>(result, page, limit, result.Count());
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
    public Entities.PagedResult<ArchiveEntry> RetrieveArchiveList(FilterModel<ArchiveEntry> filterModel, SortModel<ArchiveEntry> sortModel, int limit = 100, int page = 1)
    {
        var result = dbContext.ArchiveEntries.Include(s => s.Plant).Include(s => s.Medium).AsQueryable();

        if (sortModel != null)
            result = result.OrderBy(sortModel.FieldName + " " + sortModel.Direction);

        if (filterModel != null)
            return new Entities.PagedResult<ArchiveEntry>(result.Where(filterModel.BuildFilterFunction()), page, limit, result.Count());

        return new Entities.PagedResult<ArchiveEntry>(result, page, limit, result.Count());
    }

    [QueryRoot("history")]
    public Entities.PagedResult<ArchiveEntry> GetStockHistory(string history, int limit = 100, int page = 1)
    {
        var entries = new List<string>();
        var entry = new StringBuilder();
        foreach(char c in history)
        {
            entry.Append(c);
            if(c == ';')
                entries.Add(entry.ToString());
        }

        var result = dbContext.ArchiveEntries.Include(s => s.Plant).Include(s => s.Medium).AsQueryable().Where(a => entries.Contains(a.History));

        return new Entities.PagedResult<ArchiveEntry>(result, page, limit, result.Count());
    }

    [MutationRoot("split")]
    public SplitResponse SplitStock(int id, IEnumerable<StockEntry> newEntries, string? reason)
    {
        var original = dbContext.StockEntries.Find(id);

        if (original == null)
            throw new Exception("Stock entry not found.");

        dbContext.StockEntries.Remove(original);
        dbContext.ArchiveEntries.Add(original.Adapt(reason));

        foreach(StockEntry entry in newEntries)
        {
            var toAdd = entry.WithoutId(original.History + original.Id + ";");

            dbContext.StockEntries.Add(toAdd);
        }

        dbContext.SaveChanges();

        return new SplitResponse() 
        { 
            New = dbContext.StockEntries.OrderBy("Id desc").Take(newEntries.Count()).Reverse(), 
            Original = dbContext.ArchiveEntries.OrderBy("Id").Last() 
        };
    }
}
