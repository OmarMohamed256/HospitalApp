using API.Helpers;
using API.Models.DTOS;
using Microsoft.AspNetCore.Mvc;
using webapi.Entities;

namespace API.Services.Interfaces
{
    public interface IUserService
    {
        Task<PagedList<UserInfoDto>> GetUsersByRoleAsync(UserParams userParams, string roleName);
        Task<PagedList<UserInfoDto>> GetAllUsersAsync(UserParams userParams);
        Task<bool> UpdateUser(UserUpdateDto userUpdateDto);
        Task<UserInfoDto> GetUserById(string userId);
    }
}