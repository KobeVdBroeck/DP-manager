using Microsoft.EntityFrameworkCore;

namespace DP_manager_API.Entities;

[Keyless]
public class StockToProcess
{
    public int Id { get; set; }
    public StockEntry StockEntry { get; set; }
    public int StockId { get; set; }
    public DateTime ProcessBy { get; set; }
    public DateTime CreatedAt { get; set; }
}