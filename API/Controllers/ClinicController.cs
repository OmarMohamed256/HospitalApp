using API.Extenstions;
using API.Helpers;
using API.Models.DTOS;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ClinicController : BaseApiController
    {
        private readonly IClinicService _clinicService;

        public ClinicController(IClinicService clinicService)
        {
            _clinicService = clinicService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ClinicDto>>> GetClinics
        ([FromQuery] ClinicParams clinicParams)
        {
            var clinics = await _clinicService.GetClinics(clinicParams);
            Response.AddPaginationHeader(clinics.CurrentPage, clinics.PageSize, clinics.TotalCount, clinics.TotalPages);
            return Ok(clinics);
        }
        [HttpGet("GetClinicsWithFirstTwoUpcomingAppointments")]
        public async Task<ActionResult<ICollection<ClinicDto>>> GetClinicsWithFirstTwoUpcomingAppointmentsAsync
        ([FromQuery] ClinicParams clinicParams)
        {
            var clinics = await _clinicService.GetClinicsWithFirstTwoUpcomingAppointmentsAsync(clinicParams);
            Response.AddPaginationHeader(clinics.CurrentPage, clinics.PageSize, clinics.TotalCount, clinics.TotalPages);
            return Ok(clinics);
        }
        [HttpPost]
        public async Task<ActionResult<CreateUpdateClinicDto>> CreateClinicAsync(CreateUpdateClinicDto clinic)
        {
            var newClinic = await _clinicService.CreateUpdateClinic(clinic);
            return Ok(newClinic);
        }
        [HttpPut]
        public async Task<ActionResult<CreateUpdateClinicDto>> UpdateClinicAsync(CreateUpdateClinicDto clinic)
        {
            var newClinic = await _clinicService.CreateUpdateClinic(clinic);
            return Ok(newClinic);
        }
        [HttpDelete("{clinicId}")]
        public async Task<ActionResult> DeleteClinicAsync(int clinicId)
        {
            await _clinicService.DeleteClinic(clinicId);
            return NoContent();
        }
    }
}