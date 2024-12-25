using DP_manager_API.Entities;

namespace DP_manager_API.Adapters;

public static class UrgencyAdapter
{
    public static Urgency Adapt(this DateTime dateTime)
    {
        var date = dateTime.Date;
        if (date < DateTime.Now.Date)
            return Urgency.PastDue;
        if(date > DateTime.Now.Date)
            return Urgency.Tomorrow;
        return Urgency.Today;
    }
}
