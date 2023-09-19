using API.Helpers;
using API.Models.DTOS;

namespace API.Services.Interfaces
{
    public interface IUserService
    {
        Task<PagedList<UserInfoDto>> GetAllUsersAsync(UserParams userParams);
        Task<bool> UpdateUser(UserUpdateDto userUpdateDto);
        Task<UserInfoDto> GetUserById(string userId);
    }
}