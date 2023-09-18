using API.Models.DTOS;

namespace API.Services.Interfaces
{
    public interface IDoctorServiceService
    {
        Task<IEnumerable<DoctorServiceDto>> GetDoctorServiceByDoctorId(int doctorId);
    }
}