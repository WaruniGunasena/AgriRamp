using System.ComponentModel.DataAnnotations;

namespace AgriState_MiniTracker.Models
{
    public class Location
    {
        public int LocationId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Code { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
    }
}