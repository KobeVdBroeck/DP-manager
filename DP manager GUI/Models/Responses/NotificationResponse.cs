using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_manager.Models
{
    public class NotificationResponse : IGraphQlResponse
    {
        public PagedResponse<IEnumerable<Notification>> Notifications { get; set; }

        public IEnumerable GetData()
        {
            return Notifications.Result;
        }

        public (int page, int pageCount) GetPageInfo()
        {
            return (Notifications.CurrentPage, Notifications.PageCount);
        }
    }
}