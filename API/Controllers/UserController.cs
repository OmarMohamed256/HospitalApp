using API.Extenstions;
using API.Helpers;
using API.Models.DTOS;
using API.Services.Interfaces;
using HospitalApp.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut]
        [Authorize(Policy = Polices.RequireReceptionistRole)]
        public async Task<ActionResult> UpdateUserAsAdmin(UserUpdateDto userUpdateDto)
        {
            if (await _userService.UpdateUser(userUpdateDto)) return NoContent();
            return BadRequest("Failed to update user");
        }
        [HttpGet]
        [Route("all")]
        [Authorize(Policy = Polices.RequireReceptionistRole)]
        public async Task<ActionResult<PagedList<UserInfoDto>>> GetUsers([FromQuery] UserParams userParams)
        {
            var users = await _userService.GetAllUsersAsync(userParams);
            Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
            return Ok(users);
        }

        [HttpGet]
        [Route("byRole/{roleName}")] // Define a specific route for this action
        [Authorize(Policy = Polices.RequireReceptionistRole)]
        public async Task<ActionResult<IEnumerable<UserInfoDto>>> GetUsersWithRole([FromQuery] UserParams userParams, string roleName)
        {
            var users = await _userService.GetUsersByRoleAsync(userParams, roleName);
            Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
            return Ok(users);
        }
        [HttpGet]
        [Route("{Id}", Name = "GetUser")] // Define a specific route for this action
        [Authorize(Policy = Polices.RequireDoctorRole)]
        public async Task<ActionResult<UserInfoDto>> GetUserById(string Id)
        {
            var user = await _userService.GetUserById(Id);
            if (user == null) return NotFound("User with this id is not found");
            return Ok(user);
        }
    }
}