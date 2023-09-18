using System.Transactions;
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

            // Start a new transaction
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // Add the service to the database
                    _serviceRepository.AddService(service);

                    // Save changes to the database
                    if (await _serviceRepository.SaveAllAsync())
                    {
                        // Create doctor services for the newly added service
                        await _serviceRepository.CreateDoctorServicesForService(service);

                        // Save changes to create doctor services
                        if (await _serviceRepository.SaveAllAsync())
                        {
                            // Commit the transaction
                            scope.Complete();

                            serviceDto.Id = service.Id;
                            return serviceDto;
                        }
                        else
                        {
                            // Rollback the transaction
                            scope.Dispose();
                            throw new ApiException(500, "Failed to create doctor services for the service");
                        }
                    }
                    else
                    {
                        // Rollback the transaction
                        scope.Dispose();
                        throw new ApiException(500, "Failed to add service");
                    }
                }
                catch (Exception ex)
                {
                    // Handle and log the exception
                    // Rollback the transaction
                    scope.Dispose();
                    throw; // Re-throw the exception to be handled by the calling code
                }
            }
        }

        public async Task<ActionResult<ServiceDto>> UpdateServiceAsync(ServiceDto serviceDto)
        {
            var service = _mapper.Map<Service>(serviceDto);

            _serviceRepository.UpdateService(service);

            // Call UpdateDoctorServicesForService after updating the service
            await _serviceRepository.UpdateDoctorServicesForService(service);

            if (await _serviceRepository.SaveAllAsync())
            {
                serviceDto.Id = service.Id;
                return serviceDto;
            }
            else
            {
                throw new ApiException(500, "Failed to update service");
            }
        }




        public async Task<bool> DeleteServiceAsync(int id)
        {
            var service = await _serviceRepository.GetServiceById(id) ?? throw new ApiException(404, "Service Does not Exist");
            _serviceRepository.DeleteService(service);
            if (await _serviceRepository.SaveAllAsync()) return true;
            return false;
        }

        public async Task<PagedList<ServiceDto>> GetServicesAsync(ServiceParams serviceParams)
        {
            var services = await _serviceRepository.GetServicesAsync(serviceParams);
            var servicesDtos = _mapper.Map<IEnumerable<ServiceDto>>(services);
            return new PagedList<ServiceDto>(servicesDtos, services.TotalCount, services.CurrentPage, services.PageSize);
        }
    }
}