namespace API.Models.Entities
{
    public class InvoiceDoctorServiceSupplyOrders
    {
        public int Id { get; set; }
        public int? InvoiceDoctorServiceId { get; set; }
        public virtual InvoiceDoctorService? InvoiceDoctorService {get; set;}
        public int? SupplyOrderId { get; set; }
        public virtual SupplyOrder? SupplyOrder {get; set;}
        public int  QuantityUsed { get; set; }
        public decimal  ItemPrice { get; set; }
        public decimal  TotalPrice { get; set; }
    }
}