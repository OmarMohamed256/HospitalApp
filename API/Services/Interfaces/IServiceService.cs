using API.Helpers;
using API.Models.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace API.Services.Interfaces
{
    public interface IServiceService
    {
        Task<ActionResult<ServiceDto>> CreateServiceAsync(ServiceDto serviceDto);
        Task<PagedList<ServiceDto>> GetServicesAsync(ServiceParams serviceParams);

    }
}