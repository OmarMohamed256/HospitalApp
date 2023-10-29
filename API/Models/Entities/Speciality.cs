using API.Models.Entities;
using webapi.Entities;

namespace HospitalApp.Models.Entities
{
    public class Speciality
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<AppUser>? Doctors { get; set; }
        public virtual ICollection<Appointment>? Appointments { get; set; }
        public virtual ICollection<Service>? Services { get; set; }
        public virtual ICollection<InventoryItem>? InventoryItems { get; set; }
    }
}