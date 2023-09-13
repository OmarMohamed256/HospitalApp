using API.Helpers;
using API.Models.DTOS;

namespace API.Services.Interfaces
{
    public interface IUserService
    {
        Task<PagedList<UserInfoDto>> GetUsersByRoleAsync(UserParams userParams, string roleName);
        Task<PagedList<UserInfoDto>> GetAllUsersAsync(UserParams userParams);
    }
}