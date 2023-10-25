namespace API.Models.DTOS
{
    public class SupplyOrderDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal ItemPrice { get; set; }
        public string? Note { get; set; }
        public string? ItemName { get; set; }
        public int? InventoryItemId { get; set; }
        public int ConsumedQuantity { get; set; } = 0;
        public DateTime ExpiryDate { get; set; }
        public string? SupplierName { get; set; }
        public decimal SellPrice { get; set; }
    }
}