using HospitalApp.Models.Entities;
using webapi.Entities;

namespace API.Models.Entities
{
    public class Clinic
    {
        public int Id { get; set; }
        public string ClinicNumber { get; set; }
        public int ClinicSpecialityId { get; set; }
        public virtual Speciality Speciality { get; set; }
        public int? DoctorId { get; set; }
        public virtual AppUser? Doctor { get; set; }
    }
}