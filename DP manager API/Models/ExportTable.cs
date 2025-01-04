using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_manager_API.Models;

public enum ExportTable
{
    Stock,
    Archive,
    Plants,
    Mediums,
}

static class ExportTableNames
{
    public static string Get(ExportTable table)
    {
        switch (table)
        {
            case ExportTable.Stock:
                return "CurrentStock";
            case ExportTable.Archive:
                return "ArchivedStock";
            case ExportTable.Plants:
                return "Plant";
            case ExportTable.Mediums:
                return "Medium";
            default:
                return "";
        }
    }
}