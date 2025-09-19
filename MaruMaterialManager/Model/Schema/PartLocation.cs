using System.ComponentModel.DataAnnotations.Schema;

namespace MaruMaterialManager.Model.Schema
{
    /// <summary>
    /// Represents the quantity of a part at a specific location in the inventory system.
    /// </summary>
    public class PartLocation
    {
        public int PartId { get; set; }
        public Part Part { get; set; }
        
        public int LocationId { get; set; }
        public Location Location { get; set; }
        
        [Column(TypeName = "decimal(18,4)")]
        public decimal QuantityOnHand { get; set; }
        
        [Column(TypeName = "decimal(18,4)")]
        public decimal? ReorderPoint { get; set; }
    }
}
