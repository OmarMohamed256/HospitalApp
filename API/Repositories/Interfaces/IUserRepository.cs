using API.Helpers;
using webapi.Entities;

namespace API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<PagedList<AppUser>> GetAllUsersAsync(UserParams userParams);
        Task<AppUser> GetUserWithDoctorServicesAndDoctorWorkingHoursByIdAsync(int userId);
        Task<List<int>> GetDoctorsIdsBySpecialityId(int specialityId);
    }
}