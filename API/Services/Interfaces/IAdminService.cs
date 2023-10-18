using API.Models.DTOS;
using API.Models.DTOS.UserDtos;

namespace API.Services.Interfaces
{
    public interface IAdminService
    {
        Task<UserInfoDto> CreateUserAsync(CreateUserDto createUserDto);
        Task<UserInfoDto> UpdateUserAsync(UpdateUserDto updateUserDto);
        Task ToggleLockUser(string userId);
        Task ChangeUserRole(string userId, ChangeRoleDto changeRoleDto);
    }
}