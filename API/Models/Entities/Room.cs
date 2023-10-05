using webapi.Entities;

namespace API.Models.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int DoctorId { get; set; }
        public virtual AppUser Doctor { get; set; }
    }
}