using API.Models.DTOS;

namespace API.Services.Interfaces
{
    public interface IDoctorServiceService
    {
        Task<IEnumerable<DoctorServiceDto>> GetDoctorServiceWithServiceByDoctorId(int doctorId);
        Task<DoctorServiceUpdateDto> UpdateDoctorService(DoctorServiceUpdateDto doctorServiceUpdateDto);
        Task<DoctorServiceDto> GetDoctorServiceById(int Id);
    }
}