using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_manager.Models
{
    public class Notification
    {
        [Browsable(false)]
        public int Id { get; set; }
        public int StockId { get; set; }
        public DateTime ProcessBy { get; set; }
        public Urgency Urgency { get; set; }
    }
}
