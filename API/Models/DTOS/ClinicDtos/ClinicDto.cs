using webapi.Entities;

namespace API.Models.DTOS
{
    public class ClinicDto
    {
        public int Id { get; set; }
        public string ClinicNumber { get; set; }
        public int ClinicSpecialityId { get; set; }
        public int? DoctorId { get; set; }
        public ClinicDoctorDto? Doctor { get; set; }
    }
}