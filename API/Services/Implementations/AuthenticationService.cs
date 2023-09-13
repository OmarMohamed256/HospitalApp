using API.Errors;
using API.Models.DTOS;
using API.Services.Interfaces;
using HospitalApp.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Entities;

namespace API.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthenticationService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                throw new UnauthorizedAccessException("Invalid credentials");

            var userDto = new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };

            return userDto;
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
    }
}