using webapi.Entities;

namespace API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllUsersWithRoleAsync(string roleName);
        Task<IEnumerable<AppUser>> GetAllUsersAsync();

    }
}