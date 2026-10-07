using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriState_MiniTracker.Models
{
    public class InventoryItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }

        public int LocationId { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        [Required]
        [StringLength(20)]
        public string Unit { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal MinStockLevel { get; set; } = 10.00m;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Location? Location { get; set; }
        public Category? Category { get; set; }
    }
}