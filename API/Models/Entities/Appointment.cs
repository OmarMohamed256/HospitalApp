using System.ComponentModel.DataAnnotations;
using API.Models.Entities;
using webapi.Entities;

namespace HospitalApp.Models.Entities
{
    public class Appointment : ITrackableEntity
    {
        public int Id { get; set; }
        public string Status { get; set; }

        [Required]
        [StringLength(8)] // Adjust the max length as needed
        [RegularExpression("^(visit|revisit)$", ErrorMessage = "Type must be 'visit' or 'revisit'.")]
        public string Type { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateOfVisit { get; set; }
        public string? CreationNote { get; set; }
        public string? DrawUrl { get; set; }

        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int AppointmentSpecialityId  { get; set; } // Added property for SpecialityId
        public virtual AppUser Doctor { get; set; }
        public virtual AppUser User { get; set; }
        public virtual Speciality AppointmentSpeciality { get; set; } // Added navigation property for Speciality

    }
}