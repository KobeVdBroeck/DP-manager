namespace DP_manager
{
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
        public string PlantCode { get; set; }
        public int MediumId { get; set; }

        public StockEntry Clone()
        {
            return new StockEntry
            {
                Id = Id,
                Worker = Worker,
                Week = Week,
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
