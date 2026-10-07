using System.ComponentModel.DataAnnotations;

namespace AgriState_MiniTracker.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public int LocationId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Description { get; set; }

       
        public Location? Location { get; set; }
        public ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
    }
}