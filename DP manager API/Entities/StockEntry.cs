using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace DP_manager_API.Entities
{
    [Keyless]
    public class StockEntry
    {
        public int Id { get; set; }
        public string Worker { get; set; }
        public string Week { get; set; }
        public string Lab { get; set; }
        public string Location { get; set; }
        public int Recipients { get; set; }
        public int Ppr { get; set; }
        public int Category { get; set; }
        public int Phase { get; set; }
        public int Health { get; set; }
        public string History { get; set; }
        public string Remarks { get; set; }
        public Medium Medium { get; set; }
        public int MediumId { get; set; }
        public Plant Plant { get; set; }
        public string PlantCode { get; set; }
    }
}
