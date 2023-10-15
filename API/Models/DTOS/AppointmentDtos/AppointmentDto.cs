using API.Models.DTOS.AppointmentDtos;
namespace API.Models.DTOS
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string? Status { get; set; }
        public string Type { get; set; }
        public DateTime DateOfVisit { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int AppointmentSpecialityId { get; set; } // Added property for SpecialityId
        public int? InvoiceId { get; set; }
        // Navigation Properties
        public UserInfoDto? Doctor { get; set; }
        public UserInfoDto? Patient { get; set; }
        public SpecialityDto? Speciality { get; set; }
        public ICollection<AppointmentMedicineDto>? AppointmentMedicines { get; set; }
    }
}