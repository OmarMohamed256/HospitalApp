using webapi.Entities;

namespace API.Models.Entities
{
    public class DoctorWorkingHours
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public virtual AppUser Doctor { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}