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
            var user = await _userService.GetUserById(userId);
            if(user == null) return NotFound("User to update is not found");
            if(await _userService.UpdateUser(userUpdateDto, user)) return NoContent();
            return BadRequest("Failed to update user");
        }
    }
}