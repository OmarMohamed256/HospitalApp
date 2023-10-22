namespace API.Models.DTOS
{
    public class ClinicAppointmentDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime DateOfVisit { get; set; }        
    }
}