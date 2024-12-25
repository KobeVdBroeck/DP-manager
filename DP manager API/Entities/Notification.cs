namespace DP_manager_API.Entities;

public class Notification
{
    public int Id { get; set; }
    public int StockId { get; set; }
    public DateTime ProcessBy { get; set; }
    public Urgency Urgency { get; set; }
}
