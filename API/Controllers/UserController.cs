using API.Extenstions;
using API.Models.DTOS;
using API.Services.Interfaces;
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
        public async Task<ActionResult> UpdateUser(UserUpdateDto userUpdateDto)
        {
            int userId = User.GetUserId();
            if(userId == null) return BadRequest("User id not supplied");
            if(await _userService.UpdateUser(userUpdateDto, userId)) return NoContent();
            return BadRequest("Failed to update user");
        }
    }
}