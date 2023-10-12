using API.Models.DTOS;

namespace API.Services.Interfaces
{
    public interface IAdminService
    {
        Task<UserInfoDto> CreateUser(CreateUserDto createUserDto);
        Task ToggleLockUser(string userId);
    }
}