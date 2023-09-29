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
        Task<ICollection<ServiceInventoryItemDto>> GetServiceInventoryItemsByServiceId(int serviceId);
        Task<bool> DeleteServiceAsync(int id);
    }
}