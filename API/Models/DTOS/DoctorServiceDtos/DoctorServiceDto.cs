using API.Models.Entities;

namespace API.Models.DTOS
{
    public class DoctorServiceDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int ServiceId { get; set; }
        public decimal DoctorPercentage { get; set; }
        public decimal HospitalPercentage { get; set; }
        public ServiceDto? Service { get; set; }
    }
}