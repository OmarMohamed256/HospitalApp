namespace API.Models.DTOS
{
    public class ServiceInventoryItemDto
    {
        public int ServiceId { get; set; }
        public int InventoryItemId { get; set; }
        public int QuantityNeeded { get; set; }
        public InventoryItemDto? InventoryItem { get; set; }
    }
}