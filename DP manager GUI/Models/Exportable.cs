using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_manager.Models
{
    public class Exportable
    {
        public static string GetTableName(string t) 
        {
            switch (t)
            {
                case "ArchiveEntry":
                    return "Archive";
                case "StockEntry":
                    return "Stock";
                default:
                    return "";
            }
        }
    }
}
