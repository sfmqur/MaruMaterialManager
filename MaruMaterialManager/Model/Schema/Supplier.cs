using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MaruMaterialManager.Model.Schema
{
    /// <summary>
    /// Represents a supplier or vendor that provides parts to the inventory.
    /// </summary>
    public class Supplier
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(200)]
        public string ContactPerson { get; set; }
        
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        
        [MaxLength(50)]
        public string Phone { get; set; }
        
        [MaxLength(500)]
        public string Address { get; set; }
        
        // Navigation property
        public ICollection<Part> Parts { get; set; }
    }
}
