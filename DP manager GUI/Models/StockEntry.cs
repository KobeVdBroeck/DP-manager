using DP_manager.Interfaces;
using DP_manager.Models;
using System;
using System.ComponentModel;

namespace DP_manager
{
    
    public class StockEntry : Exportable
    {
        [Browsable(false)]
        public int Id { get; set; }
        public string Worker { get; set; }
        public DateTime Timestamp { get; set; }
        public string Lab { get; set; }
        public string Location { get; set; }
        public int Recipients { get; set; }
        public int Ppr { get; set; }
        public Category Category { get; set; }
        public Phase Phase { get; set; }
        public Health Health { get; set; }
        [Browsable(false)]
        public string History { get; set; }
        public string Remarks { get; set; }
        public string PlantCode { get; set; }
        public int MediumId { get; set; }

        public StockEntry Clone()
        {
            return new StockEntry
            {
                Id = Id,
                Worker = Worker,
                Timestamp = Timestamp,
                Lab = Lab,
                Location = Location,
                Recipients = Recipients,
                Ppr = Ppr,
                Category = Category,
                Phase = Phase,
                Health = Health,
                History = History,
                PlantCode = PlantCode,
                MediumId = MediumId
            };
        }
    }
}
