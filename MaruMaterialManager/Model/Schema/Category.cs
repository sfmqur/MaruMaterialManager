using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MaruMaterialManager.Model.Schema
{
    /// <summary>
    /// Represents a category for organizing parts in the inventory system.
    /// </summary>
    public class Category
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        [MaxLength(500)]
        public string Description { get; set; }
        
        // Navigation property
        public ICollection<Part> Parts { get; set; }
    }
}
