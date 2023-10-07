namespace API.Models.DTOS
{
    public class CreateInvoiceDto
    {
        public string PaymentMethod { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal TotalPaid { get; set; }
        public int AppointmentId { get; set; }
        public ICollection<CreateCustomItemDto>? CustomItems { get; set; }
        public ICollection<CreateInvoiceDoctorServiceDto>? InvoiceDoctorServices { get; set; }
    }
}