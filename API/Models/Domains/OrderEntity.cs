using API.Models.Entities;

namespace API.Models.Domains
{
    public abstract class OrderEntity : ITrackableEntity
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal SellPrice { get; set; }
        public string? Note { get; set; }
        public string ItemName { get; set; }
        public int? InventoryItemId { get; set; }
        public virtual InventoryItem? InventoryItem { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}