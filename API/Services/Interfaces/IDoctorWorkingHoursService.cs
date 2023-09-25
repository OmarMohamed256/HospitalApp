using API.Models.DTOS;

namespace API.Services.Interfaces
{
    public interface IDoctorWorkingHoursService
    {
        Task<IEnumerable<DoctorWorkingHoursDto>> GetDoctorWorkingHoursByDoctorIdAsync(int doctorId);
    }
}