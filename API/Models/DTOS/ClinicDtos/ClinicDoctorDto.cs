using API.Models.Entities;
using webapi.Entities;

namespace API.Models.DTOS
{
    public class ClinicDoctorDto
    {
        public int ClinicId { get; set; }
        public int DoctorId { get; set; }
        public AppUser? Doctor { get; set; }
    }
}