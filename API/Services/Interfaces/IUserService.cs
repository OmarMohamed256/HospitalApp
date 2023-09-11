using API.Models.DTOS;

namespace API.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserInfoDto>> GetAllUsersWithRole(string roleName);
    }
}