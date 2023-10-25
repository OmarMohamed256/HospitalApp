using API.Models.Domains;

namespace API.Models.Entities
{
    public class SupplyOrder : OrderEntity
    {
        public decimal ItemPrice { get; set; }
        public string? SupplierName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public virtual ICollection<InvoiceDoctorServiceSupplyOrders>? InvoiceDoctorServiceSupplyOrders { get; set; }
        public virtual ICollection<SellOrderConsumesSupplyOrder>? SellOrderConsumesSupplyOrders { get; set; }
        public int ConsumedQuantity { get; set; } = 0;
    }
}