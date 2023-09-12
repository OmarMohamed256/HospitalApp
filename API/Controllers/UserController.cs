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

        [HttpGet]
        [Authorize(Policy = Polices.RequireReceptionistRole)]
        public async Task<ActionResult<PagedList<UserInfoDto>>> GetUsers([FromQuery]UserParams userParams)
        {
            var users = await _userService.GetAllUsersAsync(userParams);
            Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
            return Ok(users);
        }

        [HttpGet("{roleName}")]
        [Authorize(Policy = Polices.RequireReceptionistRole)]
        public async Task<ActionResult<IEnumerable<UserInfoDto>>> GetUsersWithRole([FromQuery]UserParams userParams, string roleName)
        {
            var users = await _userService.GetUsersByRoleAsync(userParams, roleName);
            return Ok(users);
        }

    }
}