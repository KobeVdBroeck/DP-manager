﻿using OfficeOpenXml;
using System.Drawing;
using System.Text;



namespace DP_manager_API.Data
{
    public static class XlsxImport
    {
        public static void ImportCurrentStock(string file)
        {
            using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(file)))
            {
                var myWorksheet = xlPackage.Workbook.Worksheets.First(); //select sheet here
                var totalRows = myWorksheet.Dimension.End.Row;
                var totalColumns = myWorksheet.Dimension.End.Column;

                var sb = new StringBuilder(); //this is your data
                for (int rowNum = 1; rowNum <= totalRows; rowNum++) //select starting row here
                {
                    var row = myWorksheet.Cells[rowNum, 1, rowNum, totalColumns].Select(c => c.Value == null ? string.Empty : c.Value.ToString());
                    sb.AppendLine(string.Join(",", row));
                }
            }
        }
    }
}
