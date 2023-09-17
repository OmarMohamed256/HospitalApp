using HospitalApp.Models.Entities;

namespace API.Models.Entities
{
    public class Invoice : ITrackableEntity
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalDue { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAfterDiscount { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalRemaining { get; set; }
        public DateTime FinalizationDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; }
        public virtual ICollection<CustomItem>? CustomItems { get; set; }
        public virtual ICollection<InvoiceDoctorService>? InvoiceDoctorServices { get; set; }

    }
}