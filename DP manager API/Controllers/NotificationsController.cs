using DP_manager_API.Adapters;
using DP_manager_API.Data;
using DP_manager_API.Entities;
using DP_manager_API.Models;
using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace DP_manager_API.Controllers;

public class NotificationsController(AppDbContext dbContext) : GraphController
{
    [QueryRoot("notifications")]
    public Models.PagedResult<Notification> RetrieveNotifications(Urgency? urgency, FilterModel<StockToProcess> filterModel, SortModel<StockToProcess> sortModel, int limit = 100, int page = 1)
    {
        var result = dbContext.StockToProcessEntries.Include(s => s.StockEntry).AsQueryable();

        if(urgency != null)
            switch (urgency)
            {
                case Urgency.PastDue:
                    result = result.Where(r => r.ProcessBy < DateTime.UtcNow);
                break;

                case Urgency.Today:
                    result = result.Where(r => r.ProcessBy.Date == DateTime.UtcNow.Date && r.ProcessBy > DateTime.UtcNow);
                    break;

                case Urgency.Tomorrow:
                    result = result.Where(r => r.ProcessBy >= DateTime.UtcNow.Date.AddDays(1));
                    break;

                default:
                    break;
            }


        if (sortModel != null)
            result = result.OrderBy(sortModel.FieldName + " " + sortModel.Direction);

        if (filterModel != null)
            return new Models.PagedResult<Notification>(result.Where(filterModel.BuildFilterFunction()).Adapt(urgency), page, limit, result.Count());


        return new Models.PagedResult<Notification>(result.Adapt(urgency), page, limit, result.Count());
    }

    [QueryRoot("pastDueCount")]
    public int GetPastDueCount()
    {
        return dbContext.StockToProcessEntries.Where(r => r.ProcessBy < DateTime.UtcNow).Count();
    }

    [QueryRoot("newNotifications")]
    public IEnumerable<Notification> GetNewNotifications(DateTime? since)
    {
        if(since == null) 
            since = DateTime.UtcNow;
        else if (since?.Kind != DateTimeKind.Utc)
            throw new ArgumentException("Date must be UTC.");

        return dbContext.StockToProcessEntries.Where(r => r.CreatedAt > since).Adapt(null).OrderBy(r => r.ProcessBy);
    }
}
