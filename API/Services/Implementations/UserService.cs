using API.Errors;
using API.Helpers;
using API.Models.DTOS;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using HospitalApp.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<UserDto> CreatePatientAsync(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) throw new BadRequestException("Username is already taken.");
            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                Age = registerDto.Age,
                Email = registerDto.Email,
                FullName = registerDto.FullName,
                Gender = registerDto.Gender,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                string errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BadRequestException(errorMessage);
            }

            var roleResults = await _userManager.AddToRoleAsync(user, Roles.Patient);

            if (!roleResults.Succeeded)
            {
                string errorMessage = string.Join(", ", roleResults.Errors.Select(e => e.Description));
                throw new BadRequestException(errorMessage);
            }
            
            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };
        }
        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(u => u.UserName == username.ToLower());
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
    }
}