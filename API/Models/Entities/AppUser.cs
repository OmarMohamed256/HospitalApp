using System.ComponentModel.DataAnnotations;
using API.Models.Entities;
using HospitalApp.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace webapi.Entities
{
    public class AppUser : IdentityUser<int>, ITrackableEntity
    {
        public string FullName { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }

        [Required]
        [StringLength(10)] // Adjust the max length as needed
        [RegularExpression("^(female|male)$", ErrorMessage = "Gender must be 'female' or 'male'.")]
        public string Gender { get; set; }
        public int? Age { get; set; }
        public decimal? PriceVisit { get; set; } = 0;
        public decimal? PriceRevisit { get; set; } = 0;
        // Navigation property for the doctor
        public int? DoctorSpecialityId { get; set; }
        public virtual Speciality? DoctorSpeciality { get; set; } // Added navigation property for Speciality
        // Navigation Properties for Appointments booked by the user
        public virtual ICollection<Appointment>? BookedAppointments { get; set; }

        // Navigation Properties for Appointments where the user is the doctor
        public virtual ICollection<Appointment>? BookedWithAppointments { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public virtual ICollection<DoctorService>? DoctorServices { get; set; }
        public virtual ICollection<DoctorWorkingHours>? DoctorWorkingHours { get; set; }
        public virtual ICollection<Image>? Images { get; set; }
        public virtual ICollection<ClinicDoctor>? ClinicDoctors { get; set; }
    }
}
