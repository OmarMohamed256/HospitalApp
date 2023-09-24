using webapi.Entities;

namespace API.Models.Entities
{
    public class DoctorService
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int ServiceId { get; set; }
        public decimal DoctorPercentage { get; set; }
        public decimal HospitalPercentage { get; set; }
        public virtual AppUser Doctor { get; set; }
        public virtual Service Service { get; set; }
        public virtual ICollection<InvoiceDoctorService>? InvoiceDoctorService { get; set; }
    }
}