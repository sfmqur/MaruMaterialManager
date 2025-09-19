using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaruMaterialManager.Model.Schema
{
    /// <summary>
    /// Represents a part or material in the inventory system.
    /// </summary>
    public class Part
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string PartNumber { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        
        [MaxLength(50)]
        public string UnitOfMeasure { get; set; }
        
        [Column(TypeName = "decimal(18,4)")]
        public decimal? UnitPrice { get; set; }
        
        [Column(TypeName = "decimal(18,4)")]
        public decimal? MinimumStockLevel { get; set; }
        
        [Column(TypeName = "decimal(18,4)")]
        public decimal? MaximumStockLevel { get; set; }
        
        public int? PreferredSupplierId { get; set; }
        public Supplier PreferredSupplier { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties
        public ICollection<InventoryTransaction> InventoryTransactions { get; set; }
        public ICollection<PartLocation> PartLocations { get; set; }
        
        // Computed property for current quantity
        [NotMapped]
        public decimal CurrentQuantity { get; set; }
    }
}
