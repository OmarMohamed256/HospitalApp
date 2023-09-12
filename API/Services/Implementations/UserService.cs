using API.Models.DTOS;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using webapi.Entities;

namespace API.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserInfoDto>> GetAllUsersAsync()
        {
            IEnumerable<AppUser> users = await _userRepository.GetAllUsersAsync();

            IEnumerable<UserInfoDto> userInfoDtos = users.Select(user => new UserInfoDto
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                Gender = user.Gender,
                Age = user.Age
            });

            return userInfoDtos;
        }

        public async Task<IEnumerable<UserInfoDto>> GetUsersByRoleAsync(string roleName)
        {
            IEnumerable<AppUser> users = await _userRepository.GetAllUsersWithRoleAsync(roleName);

            IEnumerable<UserInfoDto> userInfoDtos = users.Select(user => new UserInfoDto
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                Gender = user.Gender,
                Age = user.Age
            });

            return userInfoDtos;
        }
    }
}