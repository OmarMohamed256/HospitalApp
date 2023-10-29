namespace API.Models.DTOS.ClinicDtos
{
    public class DoctorInClinicDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public ICollection<AppointmentDto>? BookedWithAppointments { get; set; }

    }
}