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
        [HttpGet("{patientId}")]
        public async Task<ActionResult<PagedList<AppointmentDto>>> GetAppointmentsByUserId
            ([FromQuery] AppointmentParams appointmentParams, [Required] int patientId)
        {
            var appointments = await _appoinmentService.GetAppointmentsForPatientAsync(appointmentParams, patientId);
            Response.AddPaginationHeader(appointments.CurrentPage, appointments.PageSize, appointments.TotalCount, appointments.TotalPages);
            return Ok(appointments);
        }

        [HttpGet]
        [Route("getUpcomingDoctorAppointmentDates/{doctorId}")]
        public async Task<ActionResult<List<DateTime>>> GetUpcomingAppointmentsForDoctorAsync(int doctorId)
        {
            var appointmentsList = await _appoinmentService.GetUpcomingAppointmentsDatesByDoctorIdAsync(doctorId);
            return Ok(appointmentsList);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<PagedList<AppointmentDto>>> GetAppointments
            ([FromQuery] AppointmentParams appointmentParams)
        {
            var appointments = await _appoinmentService.GetAppointmentsAsync(appointmentParams);
            Response.AddPaginationHeader(appointments.CurrentPage, appointments.PageSize, appointments.TotalCount, appointments.TotalPages);
            return Ok(appointments);
        }
        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> CreateAppointmentAsync(AppointmentDto appointmentDto)
        {
            return await _appoinmentService.CreateUpdateAppointmentAsync(appointmentDto);
        }
        [HttpPut]
        public async Task<ActionResult<AppointmentDto>> UpdateAppointmentAsync(AppointmentDto appointmentDto)
        {
            return await _appoinmentService.CreateUpdateAppointmentAsync(appointmentDto);
        }

    }
}