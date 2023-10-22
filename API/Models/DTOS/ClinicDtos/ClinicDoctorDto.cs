namespace API.Models.DTOS
{
    public class ClinicDoctorDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public ICollection<ClinicAppointmentDto> Appointments { get; set; }
    }
}