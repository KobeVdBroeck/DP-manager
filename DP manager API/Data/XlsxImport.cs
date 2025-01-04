using DP_manager_API.Entities;
using OfficeOpenXml;
using System.Diagnostics;

namespace DP_manager_API.Data;

public class XlsxImport
{
    AppDbContext appDbContext;

    public XlsxImport(AppDbContext dbContext)
    {
        appDbContext = dbContext;
    }

    public void ImportStock(string file, string importTarget)
    {
        using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(file)))
        {
            var myWorksheet = xlPackage.Workbook.Worksheets.First();
            var totalRows = myWorksheet.Dimension.End.Row;
            var totalColumns = myWorksheet.Dimension.End.Column;
            int counter = 1;
            string week;

            for (int rowNum = 2; rowNum <= totalRows; rowNum++)
            {
                bool isArchive = importTarget.ToLower() == "archive" || (importTarget.ToLower() == "both" && rowNum > 101);
                List<object> data = new();
                StockEntry entry;

                if (isArchive)
                    entry = new ArchiveEntry() { Reason = "No reason specified" };
                else
                    entry = new StockEntry();


                for (int i = 1; i <= totalColumns; i++)
                {
                    data.Add(myWorksheet.GetValue(rowNum, i));
                }

                if (data.Where(c => c == null).Count() > 3)
                    continue;

                using (var transaction = appDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        entry.Lab = data.ElementAt(0).ToString();
                        entry.Worker = data.ElementAt(2).ToString();
                        entry.Location = data.ElementAt(3).ToString();

                        week = data.ElementAt(7).ToString();
                        entry.Timestamp = DateTime.Parse($"01/01/20{week.Substring(0, 2)} 00:00:00")
                            .AddDays(7 * int.Parse(week.Substring(2, 2))).ToUniversalTime();

                        entry.Recipients = Convert.ToInt32(data.ElementAt(8));
                        entry.Ppr = Convert.ToInt32(data.ElementAt(10));
                        entry.Remarks = (data.ElementAt(12) ?? "").ToString();

                        var plantCode = data.ElementAt(4).ToString();
                        var plants = appDbContext.PlantEntries.Where(p => p.Code == plantCode).ToList();
                        if (plants.Count() > 0)
                            entry.Plant = plants.First();
                        else
                        {
                            var plant = new Plant() { Code = plantCode };
                            entry.Plant = plant;
                        }

                        var mediumId = Convert.ToInt32(data.ElementAt(9));
                        var mediums = appDbContext.MediumEntries.Where(p => p.Id == mediumId);
                        if (mediums.Count() > 0)
                            entry.Medium = mediums.First();
                        else
                        {
                            var medium = new Medium() { Id = mediumId };
                            entry.Medium = medium;
                        }

                        var statusCode = data.ElementAt(6).ToString();
                        entry.Category = statusCode[0] - '0';
                        entry.Phase = statusCode[1] - '0';
                        entry.Health = statusCode[2] - '0';

                        if (isArchive)
                        {
                            entry.History = (counter + 9000).ToString() + ";";
                            appDbContext.ArchiveEntries.Add((ArchiveEntry)entry);
                        }
                        else
                        {
                            entry.History = "";
                            appDbContext.StockEntries.Add(entry);
                        }

                        appDbContext.SaveChanges();
                        transaction.Commit();
                        counter++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Could not import row {rowNum}: " + ex.Message);
                        transaction.Rollback();
                    }
                }
            }
        }
        GC.Collect();
    }

    private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        throw new NotImplementedException();
    }
}
