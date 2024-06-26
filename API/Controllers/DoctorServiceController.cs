using API.Models.DTOS;
using API.Services.Interfaces;
using HospitalApp.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DoctorServiceController : BaseApiController
    {
        private readonly IDoctorServiceService _doctorServiceService;

        public DoctorServiceController(IDoctorServiceService doctorServiceService)
        {
            _doctorServiceService = doctorServiceService;
        }

        [HttpGet("services-by-doctor/{doctorId}", Name = "GetServicesByDoctorId")]
        [Authorize(Policy = Polices.RequireDoctorRole)]
        public async Task<ActionResult<IEnumerable<DoctorServiceDto>>> GetServicesByDoctorIdAsync(int doctorId)
        {
            var services = await _doctorServiceService.GetDoctorServiceWithServiceByDoctorId(doctorId);
            return Ok(services);
        }
        [HttpPut]
        [Authorize(Policy = Polices.RequireReceptionistRole)]
        public async Task<ActionResult<DoctorServiceUpdateDto>> UpdateDoctorServiceDto(DoctorServiceUpdateDto doctorServiceUpdateDto)
        {
            if (doctorServiceUpdateDto.DoctorPercentage + doctorServiceUpdateDto.HospitalPercentage > 100)
            {
                return BadRequest("The sum of Doctor Percentage and Hospital Percentage cannot exceed 100.");
            }
            return await _doctorServiceService.UpdateDoctorService(doctorServiceUpdateDto);
        }

    }
}