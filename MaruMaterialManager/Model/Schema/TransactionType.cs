namespace MaruMaterialManager.Model.Schema
{
    /// <summary>
    /// Defines the types of inventory transactions that can be recorded.
    /// </summary>
    /// <remarks>
    /// Each transaction type affects inventory in different ways:
    /// - PurchaseOrder: Increase inventory (receiving from supplier)
    /// - SalesOrder: Decrease inventory (shipping to customer)
    /// - InventoryAdjustment: Increase or decrease (for corrections)
    /// - InventoryTransferIn/Out: Move between locations
    /// - InventoryCount: For physical inventory reconciliation
    /// - Scrap: Decrease (removing damaged/obsolete items)
    /// - Return: Increase (customer returns, RMA)
    /// </remarks>
    public enum TransactionType
    {
        PurchaseOrder,
        SalesOrder,
        InventoryAdjustment,
        InventoryTransferIn,
        InventoryTransferOut,
        InventoryCount,
        Scrap,
        Return
    }
}
