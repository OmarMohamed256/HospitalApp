using HospitalApp.Models.Entities;
using webapi.Entities;

namespace API.Models.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public int RoomSpecialityId { get; set; }
        public virtual Speciality Speciality { get; set; }
        public int? DoctorId { get; set; }
        public virtual AppUser? Doctor { get; set; }
    }
}