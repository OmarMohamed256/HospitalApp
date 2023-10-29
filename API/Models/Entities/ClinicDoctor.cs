using webapi.Entities;

namespace API.Models.Entities
{
    public class ClinicDoctor
    {
        public int ClinicId { get; set; }
        public virtual Clinic Clinic { get; set; }
        public int DoctorId { get; set; }
        public virtual AppUser Doctor { get; set; }
    }
}