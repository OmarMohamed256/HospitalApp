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
        public DateTime ExpiryDate { get; set; }
    }
}