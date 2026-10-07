namespace AgriState_MiniTracker.Models
{
    public class InventoryStatsDto
    {
        public int TotalItems { get; set; }
        public int LowStockCount { get; set; }
        public decimal TotalQuantity { get; set; }
        public int LocationsCount { get; set; }
    }
}
