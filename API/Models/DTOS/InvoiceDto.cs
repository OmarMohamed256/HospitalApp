namespace API.Models.DTOS
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal TotalPaid { get; set; }
        public int AppointmentId { get; set; }
        public virtual ICollection<CustomItemDto>? CustomItems { get; set; }
        public virtual ICollection<InvoiceDoctorServiceDto>? InvoiceDoctorServices { get; set; }
    }
}