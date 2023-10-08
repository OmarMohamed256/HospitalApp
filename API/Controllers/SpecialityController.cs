using API.Models.DTOS;
using API.Services.Interfaces;
using API.SignalR;
using HospitalApp.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    public class SpecialityController : BaseApiController
    {

        private readonly ISpecialityService _specialityService;

        public SpecialityController(ISpecialityService specialityService)
        {
            _specialityService = specialityService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecialityDto>>> GetSpecialitesAsync()
        {
            var specialites = await _specialityService.GetAllSpecialitiesAsync();
            return Ok(specialites);
        }
        [HttpPost]
        public async Task<ActionResult<SpecialityDto>> AddSpecialityAsync(SpecialityDto specialityDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return await _specialityService.AddSpeciality(specialityDto);
        }

    }
}