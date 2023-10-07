namespace API.Models.DTOS
{
    public class RoomDoctorDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public ICollection<RoomAppointmentDto> Appointments { get; set; }
    }
}