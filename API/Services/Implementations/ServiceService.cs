using API.Errors;
using API.Helpers;
using API.Models.DTOS;
using API.Models.Entities;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Services.Implementations
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public ServiceService(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }
        public async Task<ActionResult<ServiceDto>> CreateServiceAsync(ServiceDto serviceDto)
        {
            // Create a new Service entity without setting the Id
            var service = _mapper.Map<Service>(serviceDto);

            _serviceRepository.AddService(service);

            if (await _serviceRepository.SaveAllAsync())
            {
                // Retrieve the generated Id and assign it to the serviceDto
                serviceDto.Id = service.Id;
                return serviceDto;
            }

            throw new ApiException(500, "Failed to add service");
        }

        public async Task<PagedList<ServiceDto>> GetServicesAsync(ServiceParams serviceParams)
        {
            var services = await _serviceRepository.GetServicesAsync(serviceParams);
            var servicesDtos = _mapper.Map<IEnumerable<ServiceDto>>(services);
            return new PagedList<ServiceDto>(servicesDtos, services.TotalCount, services.CurrentPage, services.PageSize);
        }
    }
}