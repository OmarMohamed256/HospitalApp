using API.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface IDoctorWorkingHoursRepository
    {
        Task<IEnumerable<DoctorWorkingHours>> GetDoctorWorkingHoursByDoctorIdAsync(int doctorId);
    }
}