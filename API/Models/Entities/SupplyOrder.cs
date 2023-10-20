namespace API.Models.Entities
{
    public class SupplyOrder : ITrackableEntity
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal ItemPrice { get; set; }
        public string? Note { get; set; }
        public string ItemName { get; set; }
        public int? InventoryItemId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public virtual InventoryItem? InventoryItem { get; set; }
        public virtual ICollection<InvoiceDoctorServiceSupplyOrders>? InvoiceDoctorServiceSupplyOrders { get; set; }
        public int ConsumedQuantity { get; set; } = 0;
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}