using System.ComponentModel.DataAnnotations;
using API.Extenstions;
using API.Helpers;
using API.Models.DTOS;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AppointmentController : BaseApiController
    {
        private readonly IAppoinmentService _appoinmentService;

        public AppointmentController(IAppoinmentService appoinmentService)
        {
            _appoinmentService = appoinmentService;
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<PagedList<AppointmentDto>>> GetAppointmentsByUserId
        ([FromQuery] AppointmentParams appointmentParams,[Required] int userId)
        {
            var appointments = await _appoinmentService.GetAppointmentsForUser(appointmentParams, userId);
            Response.AddPaginationHeader(appointments.CurrentPage, appointments.PageSize, appointments.TotalCount, appointments.TotalPages);
            return Ok(appointments);
        }

    }
}