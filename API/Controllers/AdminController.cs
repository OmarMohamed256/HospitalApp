using API.Models.DTOS;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // [Authorize(Policy = Polices.RequireAdminRole)]
    public class AdminController : BaseApiController
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserInfoDto>> CreateUser(CreateUserDto createUserDto)
        {
            return await _adminService.CreateUser(createUserDto);
        }

        [HttpPut("toggleLockUser/{userId}")]
        public async Task<ActionResult> ToggleLockUser(string userId)
        {
            await _adminService.ToggleLockUser(userId);
            return NoContent();
        }
    }
}
