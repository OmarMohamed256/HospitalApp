namespace API.Models.Entities
{
    public class InvoiceDoctorService
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int DoctorServiceId { get; set; }
        public int Quantity { get; set; }
        // Navigation properties
        public virtual Invoice Invoice { get; set; }
        public virtual DoctorService DoctorService { get; set; }
    }
}