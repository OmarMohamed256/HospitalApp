

using HospitalApp.Models.Entities;
using webapi.Entities;

namespace API.Models.DTOS
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateOfVisit { get; set; }
        public string? CreationNote { get; set; }
        public string? DrawUrl { get; set; }

        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int AppointmentSpecialityId { get; set; } // Added property for SpecialityId

        // Navigation Properties
        public UserInfoDto? Doctor { get; set; }
        public UserInfoDto? Patient { get; set; }

        public SpecialityDto? Speciality { get; set; }

    }
}