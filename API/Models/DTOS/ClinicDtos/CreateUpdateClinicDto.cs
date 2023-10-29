using API.Models.DTOS.ClinicDtos;

namespace API.Models.DTOS
{
    public class CreateUpdateClinicDto
    {
        public int Id { get; set; }
        public string ClinicNumber { get; set; }
        public ICollection<CreateUpdateClinicDoctorDto>? ClinicDoctors { get; set; }
    }
}