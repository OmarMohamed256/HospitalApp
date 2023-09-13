using API.Models.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace API.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ActionResult<UserDto>> Login(LoginDto loginDto);
        Task<UserDto> CreatePatientAsync(RegisterDto registerDto);

    }
}