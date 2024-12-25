namespace DP_manager_API.Data;

public class MockDataAdder
{
    AppDbContext appDbContext;

    public MockDataAdder(AppDbContext dbContext)
    {
        appDbContext = dbContext;
    }

    public void AddNotificationMocks()
    {
        var stock = appDbContext.StockEntries.Where(s => s.Id <= 30);

        foreach (var item in stock)
        {
            appDbContext.StockToProcessEntries.Add(new Entities.StockToProcess()
            {
                ProcessBy = MockNotificationsDate(item.Id).ToUniversalTime(),
                StockEntry = item,
                StockId = item.Id
            });
        }

        appDbContext.SaveChanges();
    }

    // First 10 = past due
    // Next 10 = today
    // Last 10 = tommorow
    public DateTime MockNotificationsDate(int id)
    {
        var now = DateTime.Now;

        return id <= 10 ? now.AddDays(-1) : 
            id <= 20 ? now.AddHours(1) :
            now.AddDays(1).AddHours(12);
    }
}
