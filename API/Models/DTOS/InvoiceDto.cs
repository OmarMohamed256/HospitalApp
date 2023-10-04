namespace API.Models.DTOS
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalDue { get; set; }
        public decimal CustomItemsTotalPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal TotalAfterDiscount { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalRemaining { get; set; }
        public DateTime FinalizationDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int AppointmentId { get; set; }
        public virtual ICollection<CustomItemDto>? CustomItems { get; set; }
        public virtual ICollection<InvoiceDoctorServiceDto>? InvoiceDoctorServices { get; set; }
    }
}