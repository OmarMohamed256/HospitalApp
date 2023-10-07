namespace API.Models.DTOS
{
    public class InvoiceDoctorServiceDto
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int? DoctorServiceId { get; set; }
        public int ServiceQuantity { get; set; }
        public decimal TotalDisposablesPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal ServiceSoldPrice { get; set; }
        public string ServiceName { get; set; }
    }
}