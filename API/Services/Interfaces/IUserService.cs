using API.Models.DTOS;

namespace API.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserInfoDto>> GetUsersByRoleAsync(string roleName);
        Task<IEnumerable<UserInfoDto>> GetAllUsersAsync();

    }
}