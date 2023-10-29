using API.Models.DTOS.ClinicDtos;
using webapi.Entities;

namespace API.Models.DTOS
{
    public class ClinicDoctorDto
    {
        public int ClinicId { get; set; }
        public int DoctorId { get; set; }
        public DoctorInClinicDto? Doctor { get; set; }
    }
}