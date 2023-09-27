namespace API.Models.DTOS
{
    public class CreateServiceInventoryItemDTO
    {
        public int InventoryItemId { get; set; }
        public int QuantityNeeded { get; set; }
    }
}