using HospitalApp.Models.Entities;

namespace API.Models.Entities
{
    public class AppointmentMedicine
    {
        public int AppointmentId { get; set; }
        public int MedicineId { get; set; }
        // Navigation properties
        public virtual Appointment Appointment { get; set; }
        public virtual Medicine Medicine { get; set; }
    }
}