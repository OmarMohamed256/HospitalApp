using HospitalApp.Models.Entities;

namespace API.Models.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalDue { get; set; }
        public decimal CustomItemsTotalPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal TotalAfterDiscount { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalRemaining { get; set; }
        public decimal AppointmentTypePrice { get; set; }
        public DateTime FinalizationDate { get; set; }
        public int AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; }
        public virtual ICollection<CustomItem>? CustomItems { get; set; }
        public virtual ICollection<InvoiceDoctorService>? InvoiceDoctorService { get; set; }
        public virtual ICollection<InvoiceMedicine>? InvoiceMedicines { get; set; }

    }
}