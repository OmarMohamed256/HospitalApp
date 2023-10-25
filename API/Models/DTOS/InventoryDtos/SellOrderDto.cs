namespace API.Models.DTOS.InventoryDtos
{
    public class SellOrderDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal SellPrice { get; set; }
        public string? Note { get; set; }
        public string? ItemName { get; set; }
        public int InventoryItemId { get; set; }
        public string? SoldTo { get; set; }
        public bool IncludeExpiredItems { get; set; }

    }
}