using API.Helpers;
using API.Models.DTOS;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using webapi.Entities;

namespace API.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        public UserService(IUserRepository userRepository, UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<PagedList<UserInfoDto>> GetAllUsersAsync(UserParams userParams)
        {
            PagedList<AppUser> users = await _userRepository.GetAllUsersAsync(userParams);

            var userInfoDtos = users.Select(user => new UserInfoDto
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                Gender = user.Gender,
                Age = user.Age,
                DateCreated = user.DateCreated
            });

            return new PagedList<UserInfoDto>(userInfoDtos, users.TotalCount, users.CurrentPage, users.PageSize);
        }

        public async Task<PagedList<UserInfoDto>> GetUsersByRoleAsync(UserParams userParams, string roleName)
        {
            PagedList<AppUser> users = await _userRepository.GetAllUsersWithRoleAsync(userParams, roleName);

            var userInfoDtos = users.Select(user => new UserInfoDto
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                Gender = user.Gender,
                Age = user.Age,
                DateCreated = user.DateCreated
            });

            return new PagedList<UserInfoDto>(userInfoDtos, users.TotalCount, users.CurrentPage, users.PageSize);
        }

        public async Task<bool> UpdateUser(UserUpdateDto userUpdateDto, AppUser user)
        {
            user.FullName = userUpdateDto.FullName ?? user.FullName;
            user.Age = userUpdateDto.Age ?? user.Age;
            user.Gender = userUpdateDto.Gender ?? user.Gender;
            user.PhoneNumber = userUpdateDto.PhoneNumber ?? user.PhoneNumber;
            user.SecurityStamp = Guid.NewGuid().ToString();

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return false;
            return true;
        }


        public async Task<AppUser> GetUserById(int id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }
    }
}