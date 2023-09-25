using API.Models.DTOS;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DoctorWorkingHoursController : BaseApiController
    {
        private readonly IDoctorWorkingHoursService _doctorWorkingHoursService;
        public DoctorWorkingHoursController(IDoctorWorkingHoursService doctorWorkingHoursService)
        {
            _doctorWorkingHoursService = doctorWorkingHoursService;
        }
        [HttpGet("{doctorId}")]
        public async Task<ActionResult<IEnumerable<DoctorWorkingHoursDto>>> GetDoctorWorkingHoursByDoctorIdAsync(int doctorId)
        {
            var doctorWorkingHoursList = await _doctorWorkingHoursService.GetDoctorWorkingHoursByDoctorIdAsync(doctorId);
            return Ok(doctorWorkingHoursList);
        }
    }
}