namespace API.Models.Entities
{
    public class ServiceInventoryItem
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int InventoryItemId { get; set; }
        public int QuantityNeeded { get; set; }
        public virtual InventoryItem? InventoryItem { get; set; }
        public virtual Service? Service { get; set; }
    }
}