using HospitalApp.Models.Entities;

namespace API.Models.Entities
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal DisposablesPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int ServiceSpecialityId { get; set; }
        public virtual Speciality ServiceSpeciality { get; set; }
        public virtual ICollection<DoctorService>? DoctorServices { get; set; }

    }
}