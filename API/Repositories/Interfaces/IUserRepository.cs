using API.Helpers;
using webapi.Entities;

namespace API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<PagedList<AppUser>> GetAllUsersWithRoleAsync(UserParams userParams, string roleName);
        Task<PagedList<AppUser>> GetAllUsersAsync(UserParams userParams);
    }
}