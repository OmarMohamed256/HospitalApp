using API.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        Task AddDoctorWorkingHours(IEnumerable<DoctorWorkingHours> workingHours);
        Task<bool> SaveAllAsync();
    }
}