namespace AgriState_MiniTracker.Models
{
    public class InventoryItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int LocationId { get; set; }
        public string LocationName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public decimal MinThreshold { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
