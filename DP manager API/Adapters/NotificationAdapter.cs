using DP_manager_API.Entities;

namespace DP_manager_API.Adapters;

public static class NotificationAdapter
{
    public static IEnumerable<Notification> Adapt(this IEnumerable<StockToProcess> toProcess, Urgency? urgency)
    {
        return toProcess.Select(tp =>
        {
            return new Notification()
            {
                Id = tp.Id,
                ProcessBy = tp.ProcessBy,
                StockId = tp.StockId,
                Urgency = urgency ?? tp.ProcessBy.Adapt()
            };
        });
    }
}
