using API.Errors;
using API.Models.DTOS;
using API.Services.Interfaces;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public AuthenticationService(UserManager<AppUser> userManager,
         SignInManager<AppUser> signInManager, ITokenService tokenService,
         IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;

        }

        public async Task<UserDto> Login(LoginDto loginDto)
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
            bool userExists = await UserExists(registerDto.Username);
            if (userExists) throw new BadRequestException("Username is already taken.");
            var user = _mapper.Map<AppUser>(registerDto);
            user.UserName = registerDto.Username.ToLower();

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
        public async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(u => u.UserName == username.ToLower());
        }
    }
}