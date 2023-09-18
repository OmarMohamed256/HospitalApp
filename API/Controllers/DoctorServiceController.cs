using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.DTOS;
using API.Services.Interfaces;
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
        public async Task<ActionResult<IEnumerable<DoctorServiceDto>>> GetServicesByDoctorIdAsync(int doctorId)
        {
            var services = await _doctorServiceService.GetDoctorServiceByDoctorId(doctorId);
            return Ok(services);
        }


    }
}