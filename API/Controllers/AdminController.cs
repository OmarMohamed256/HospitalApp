using API.Constants;
using API.Models.DTOS;
using API.Models.DTOS.UserDtos;
using API.Services.Interfaces;
using API.SignalR;
using HospitalApp.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    // [Authorize(Policy = Polices.RequireAdminRole)]
    public class AdminController : BaseApiController
    {
        private readonly IAdminService _adminService;
        private readonly IHubContext<AppointmentHub, INotificationHub> _appointmentNotification;

        public AdminController(IAdminService adminService, IHubContext<AppointmentHub, INotificationHub> appointmentNotification)
        {
            _adminService = adminService;
            _appointmentNotification = appointmentNotification;
        }
        [HttpPost("SimulateAppointmentStatusChange")]
        public async Task<ActionResult> SimulateAppointmentStatusChange()
        {
            await _appointmentNotification.Clients.All.SendAppointmentStatusChange
                    (new AppointmentStatusSignalR { AppointmentId = 1, Status = AppointmentStatus.Finalized });
            return Ok();
        }
        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserInfoDto>> CreateUser(CreateUserDto createUserDto)
        {
            return await _adminService.CreateUserAsync(createUserDto);
        }
        [HttpPut("UpdateUser")]
        public async Task<ActionResult<UserInfoDto>> UpdateUser(UpdateUserDto createUserDto)
        {
            return await _adminService.UpdateUserAsync(createUserDto);
        }

        [HttpPut("toggleLockUser/{userId}")]
        public async Task<ActionResult> ToggleLockUser(string userId)
        {
            await _adminService.ToggleLockUser(userId);
            return NoContent();
        }
        [HttpPut("changeUserRole/{userId}")]
        public async Task<ActionResult> ChangeRole(string userId, [FromBody] ChangeRoleDto changeRoleDto)
        {
            await _adminService.ChangeUserRole(userId, changeRoleDto);
            return NoContent();
        }
    }
}
