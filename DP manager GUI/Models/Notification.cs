using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_manager.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public DateTime ProcessBy { get; set; }
        public Urgency Urgency { get; set; }
    }
}
