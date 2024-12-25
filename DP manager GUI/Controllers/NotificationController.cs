using DP_manager.Components;
using DP_manager.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DP_manager.Controllers
{
    public class NotificationController : ResourceController<NotificationResponse, Notification>
    {
        DateTime lastFetch;

        private static readonly string GETQUERY = "query  { notifications { result { id, stockId, processBy, urgency  } currentPage, pageCount, totalCount, pageLimit }  }";
        private static readonly string STATICQUERY = "query { notifications(urgency: TODAY,  sortModel: { fieldName: \"ProcessBy\", direction: \"asc\" }, page: 1, limit: 10, ) { result { id, stockId, processBy, urgency  } currentPage, pageCount, totalCount, pageLimit }  }";
        private static readonly string COUNTQUERY = "query { pastDueCount }";
        private static readonly string NEWQUERY = "query { newNotifications(since: \"{0}\") { id, stockId, processBy, urgency }  } ";

        NotificationScheduler scheduler;

        public NotificationController() : base()
        {
            getQueryBuilder = new QueryBuilder(GETQUERY, "notifications");
            scheduler = new NotificationScheduler();
            scheduler.Notified += Scheduler_Notified;
            InitScheduler();

            SetSort("Id", "asc");
        }

        private async void Scheduler_Notified()
        {
            await UpdateNotifications();
        }

        public async Task InitScheduler()
        {
            scheduler.PastDueCount = (await GraphQlService.SendRequestAsync<CountResponse>(COUNTQUERY)).PastDueCount;
            var response = await GraphQlService.SendRequestAsync<NotificationResponse>(STATICQUERY);
            var result = response.Notifications.Result.ToList();
            scheduler.ScheduleFutureNotifications(response.Notifications.Result.ToList());
            lastFetch = DateTime.UtcNow;
        }

        public async Task UpdateNotifications()
        {
            scheduler.PastDueCount = (await GraphQlService.SendRequestAsync<CountResponse>(COUNTQUERY)).PastDueCount;
            var response = await GraphQlService.SendRequestAsync<NewNotificationsResponse>(string.Format(UpdateQueryFormat(NEWQUERY), lastFetch.ToUniversalTime().ToString()));
            scheduler.AddNewNotifications(response.NewNotifications);
            lastFetch = DateTime.UtcNow;
        }

        public override async Task<NotificationResponse> GetEntries()
        {
            var response = await base.GetEntries();
            
            return response;
        }
    }
}
