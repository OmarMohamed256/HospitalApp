using API.Models.DTOS;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using webapi.Entities;

namespace API.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserInfoDto>> GetAllUsersWithRole(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            IEnumerable<UserInfoDto> userInfoDtos = users.Select(user => new UserInfoDto
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                Gender = user.Gender
            }).ToList();

            return userInfoDtos;
        }
    }
}