using API.Extenstions;
using API.Helpers;
using API.Models.DTOS;
using API.Services.Interfaces;
using HospitalApp.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ServiceController : BaseApiController
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpPost]
        [Authorize(Policy = Polices.RequireAdminRole)]
        public async Task<ActionResult<ServiceDto>> CreateUpdateServiceAsync(ServiceDto serviceDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return await _serviceService.CreateServiceAsync(serviceDto);
        }

        [HttpGet]
        [Authorize(Policy = Polices.RequireReceptionistRole)]
        public async Task<ActionResult<PagedList<ServiceDto>>> GetServicesAsync([FromQuery] ServiceParams serviceParams)
        {
            var services = await _serviceService.GetServicesAsync(serviceParams);
            Response.AddPaginationHeader(services.CurrentPage, services.PageSize, services.TotalCount, services.TotalPages);
            return Ok(services);
        }

        [HttpDelete]
        [Authorize(Policy = Polices.RequireAdminRole)]
        public async Task<ActionResult> DeleteServiceAsync(int serviceId)
        {
            var result = await _serviceService.DeleteServiceAsync(serviceId);
            if(result) return Ok();
            else return BadRequest("Failed deleting service");
        }
    }
}