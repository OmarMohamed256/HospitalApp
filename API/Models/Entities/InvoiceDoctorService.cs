namespace API.Models.Entities
{
    public class InvoiceDoctorService
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice {get; set;}
        public int? DoctorServiceId { get; set; }
        public virtual DoctorService? DoctorService {get; set;}
        public int ServiceQuantity { get; set; }
        public decimal TotalDisposablesPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal ServiceSoldPrice { get; set; }
        public string ServiceName { get; set; }
        public virtual ICollection<InvoiceDoctorServiceSupplyOrders>? InvoiceDoctorServiceSupplyOrders { get; set; }
    }
}