using System.ComponentModel.DataAnnotations;

namespace AgriState_MiniTracker.Models
{
    public class CreateInventoryItemDto
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int LocationId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Range(0, 999999)]
        public decimal Quantity { get; set; }

        [Required]
        [StringLength(20)]
        public string Unit { get; set; } = "Units";

        [Range(0, 999999)]
        public decimal MinThreshold { get; set; } = 5.00m;
    }
}
