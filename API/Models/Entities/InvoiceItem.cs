namespace API.Models.Entities
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public int? InvoiceId { get; set; }
        public int? DoctorServiceId { get; set; }
        public int Quantity { get; set; }
        public decimal ServicePrice { get; set; }
        public string ServiceName { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual DoctorService DoctorService { get; set; }
    }
}