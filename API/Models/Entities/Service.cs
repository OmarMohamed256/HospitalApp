using HospitalApp.Models.Entities;

namespace API.Models.Entities
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal DoctorPercentage { get; set; }
        public decimal HospitalPercentage { get; set; }
        public decimal DisposablesPercentage { get; set; }
        public decimal TotalPrice { get; set; }
        public int ServiceSpecialityId { get; set; }
        public virtual Speciality ServiceSpeciality { get; set; }
        public virtual ICollection<InvoiceServiceJoin> InvoiceServiceJoins { get; set; }

    }
}