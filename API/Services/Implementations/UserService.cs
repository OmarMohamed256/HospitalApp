using API.Helpers;
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

        public async Task<PagedList<UserInfoDto>> GetAllUsersAsync(UserParams userParams)
        {
            PagedList<AppUser> users = await _userRepository.GetAllUsersAsync(userParams);

            var userInfoDtos = users.Select(user => new UserInfoDto
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                Gender = user.Gender,
                Age = user.Age
            });

            return new PagedList<UserInfoDto>(userInfoDtos, users.TotalCount, users.CurrentPage, users.PageSize);
        }

        public async Task<PagedList<UserInfoDto>> GetUsersByRoleAsync(UserParams userParams, string roleName)
        {
            PagedList<AppUser> users = await _userRepository.GetAllUsersWithRoleAsync(userParams, roleName);

            var userInfoDtos = users.Select(user => new UserInfoDto
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                Gender = user.Gender,
                Age = user.Age
            });

            return new PagedList<UserInfoDto>(userInfoDtos, users.TotalCount, users.CurrentPage, users.PageSize);
        }
    }
}