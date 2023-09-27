using API.Helpers;
using API.Models.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace API.Services.Interfaces
{
    public interface IServiceService
    {
        Task<CreateServiceDTO> CreateServiceAsync(CreateServiceDTO serviceDto);
        Task<CreateServiceDTO> UpdateServiceAsync(CreateServiceDTO serviceDto);
        Task<PagedList<ServiceDto>> GetServicesAsync(ServiceParams serviceParams);
        Task<bool> DeleteServiceAsync(int id);
    }
}