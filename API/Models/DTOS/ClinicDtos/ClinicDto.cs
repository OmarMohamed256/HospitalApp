using API.Models.Entities;

namespace API.Models.DTOS
{
    public class ClinicDto
    {
        public int Id { get; set; }
        public string ClinicNumber { get; set; }
        public ICollection<ClinicDoctorDto>? ClinicDoctors { get; set; }
    }
}