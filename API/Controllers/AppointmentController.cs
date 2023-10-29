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
        [HttpGet("getPatientAppointmentsById/{patientId}")]
        public async Task<ActionResult<PagedList<AppointmentDto>>> GetAppointmentsByUserId
            ([FromQuery] AppointmentParams appointmentParams, [Required] int patientId)
        {
            var appointments = await _appoinmentService.GetAppointmentsForPatientAsync(appointmentParams, patientId);
            Response.AddPaginationHeader(appointments.CurrentPage, appointments.PageSize, appointments.TotalCount, appointments.TotalPages);
            return Ok(appointments);
        }
        [HttpGet("getDoctorAppointmentsById/{doctorId}")]
        public async Task<ActionResult<PagedList<AppointmentDto>>> GetAppointmentsByDoctorId
            ([FromQuery] AppointmentParams appointmentParams, [Required] int doctorId)
        {
            var appointments = await _appoinmentService.GetAppointmentsForDoctorAsync(appointmentParams, doctorId);
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
        [HttpGet]
        [Route("getAppointmentById/{appointmentId}")]
        public async Task<ActionResult<PagedList<AppointmentDto>>> GetAppointmentById
            (int appointmentId)
        {
            var appointment = await _appoinmentService.GetAppointmentByIdAsync(appointmentId);
            if (appointment == null) return BadRequest("Appointment does not exist");
            return Ok(appointment);
        }
        [HttpGet]
        [Route("getFirstTwoUpcomingAppointmentsForDoctorById/{doctorId}")]
        public async Task<ActionResult<ICollection<AppointmentDto>>> GetFirstTwoUpcomingAppointmentsForDoctorById
            (int doctorId)
        {
            var appointments = await _appoinmentService.GetFirstTwoUpcomingAppointmentsForDoctorById(doctorId);
            return Ok(appointments);
        }
        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> CreateAppointmentAsync(AppointmentDto appointmentDto)
        {
            return await _appoinmentService.CreateUpdateAppointmentAsync(appointmentDto);
        }
        [HttpPut]
        // [Authorize(Policy = Polices.RequireDoctorRole)]
        public async Task<ActionResult<AppointmentDto>> UpdateAppointmentAsync(AppointmentDto appointmentDto)
        {
            return await _appoinmentService.CreateUpdateAppointmentAsync(appointmentDto);
        }
        [HttpDelete("{appointmentId}")]
        public async Task<ActionResult> DeleteAppointmentAsync(int appointmentId)
        {
            await _appoinmentService.DeleteAppointment(appointmentId);
            return NoContent();
        }
    }
}