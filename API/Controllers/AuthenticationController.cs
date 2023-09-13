using API.Models.DTOS;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthenticationController : BaseApiController
    {
        private IAuthenticationService _authService;
        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var userDto = await _authService.Login(loginDto);
            return userDto;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> CreatePatientAsync(RegisterDto registerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return await _authService.CreatePatientAsync(registerDto);
        }

    }
}