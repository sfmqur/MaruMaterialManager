using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaruMaterialManager.Model.Schema
{
    /// <summary>
    /// Represents an inventory transaction, which is a change in inventory quantity.
    /// </summary>
    public class InventoryTransaction
    {
        public int Id { get; set; }
        
        public int PartId { get; set; }
        public Part Part { get; set; }
        
        public int? LocationId { get; set; }
        public Location Location { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Quantity { get; set; }
        
        public TransactionType TransactionType { get; set; }
        
        public string ReferenceNumber { get; set; }
        
        [MaxLength(1000)]
        public string Notes { get; set; }
        
        public DateTime TransactionDate { get; set; }
        
        public string CreatedBy { get; set; }
    }
}
