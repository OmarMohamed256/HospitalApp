using API.Models.DTOS;

namespace API.Services.Interfaces
{
    public interface IAdminService
    {
        Task<CreateUserDto> CreateUser(CreateUserDto createUserDto);
    }
}