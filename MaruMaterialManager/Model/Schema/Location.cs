using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MaruMaterialManager.Model.Schema
{
    /// <summary>
    /// Represents a physical location where parts are stored in the inventory system.
    /// </summary>
    public class Location
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        [MaxLength(500)]
        public string Description { get; set; }
        
        // Navigation properties
        public ICollection<InventoryTransaction> InventoryTransactions { get; set; }
        public ICollection<PartLocation> PartLocations { get; set; }
    }
}
