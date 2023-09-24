using System.Transactions;
using API.Constants;
using API.Errors;
using API.Helpers;
using API.Models.DTOS;
using API.Models.Entities;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services.Implementations
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly IDoctorServiceRepository _doctorServiceRepository;
        private readonly IUserRepository _doctorRepository;
        public ServiceService(IServiceRepository serviceRepository, IMapper mapper,
        IDoctorServiceRepository doctorServiceRepository, IUserRepository doctorRepository)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _doctorServiceRepository = doctorServiceRepository;
            _doctorRepository = doctorRepository;
        }
        public async Task<ServiceDto> CreateServiceAsync(ServiceDto serviceDto)
        {
            // Create a new Service entity without setting the Id
            var service = _mapper.Map<Service>(serviceDto);

            // Start a new transaction
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                // Add the service to the database
                _serviceRepository.AddService(service);
                bool saveService = await _serviceRepository.SaveAllAsync();
                // Save changes to the database
                if (saveService)
                {
                    var serviceDtoNew = await HandleDoctorServicesAsync(service, serviceDto);
                    scope.Complete();
                    return serviceDtoNew;
                }
                else
                {
                    // Rollback the transaction
                    scope.Dispose();
                    throw new ApiException(HttpStatusCode.InternalServerError, "Failed to add service");
                }
            }
            catch (Exception)
            {
                // Handle and log the exception
                // Rollback the transaction
                scope.Dispose();
                throw; // Re-throw the exception to be handled by the calling code
            }
        }

        public async Task<ServiceDto> UpdateServiceAsync(ServiceDto serviceDto)
        {
            // Start a new transaction
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var service = _mapper.Map<Service>(serviceDto);
                _serviceRepository.UpdateService(service);

                var servicesToDelete = await _doctorServiceRepository.GetDoctorServiceByServiceId(service.Id);

                if (servicesToDelete == null || !servicesToDelete.Any())
                {
                    var result = await HandleDoctorServicesAsync(service, serviceDto);
                    scope.Complete();
                    return result;
                }

                // Call UpdateDoctorServicesForService after updating the service
                _doctorServiceRepository.DeleteDoctorServices(servicesToDelete);
                bool deleteDoctorsResult = await _serviceRepository.SaveAllAsync();
                if (deleteDoctorsResult)
                {
                    var result = await HandleDoctorServicesAsync(service, serviceDto);
                    scope.Complete();
                    return result;
                }
                else throw new ApiException(HttpStatusCode.InternalServerError, "Failed to update service");
            }
            catch (Exception)
            {
                // If an exception occurs, the transaction will be rolled back when the TransactionScope is disposed
                throw;
            }
        }

        private async Task<ServiceDto> HandleDoctorServicesAsync(Service service, ServiceDto serviceDto)
        {
            var doctorsWithSpecialityId = await _doctorRepository.GetDoctorsIdsBySpecialityId(service.ServiceSpecialityId);
            if (!doctorsWithSpecialityId.Any()) return _mapper.Map<ServiceDto>(service);
            else
            {
                await _doctorServiceRepository.CreateDoctorServicesForService(service.Id, doctorsWithSpecialityId);
                bool createDoctorServicesResult = await _serviceRepository.SaveAllAsync();
                if (createDoctorServicesResult) return _mapper.Map<ServiceDto>(service);
            }
            throw new ApiException(HttpStatusCode.InternalServerError, "Failed to add doctor services");
        }

        public async Task<bool> DeleteServiceAsync(int id)
        {
            var service = await _serviceRepository.GetServiceById(id) ?? throw new ApiException(HttpStatusCode.NotFound, "Service Does not Exist");
            _serviceRepository.DeleteService(service);
            bool deleteServiceResult = await _serviceRepository.SaveAllAsync();
            if (deleteServiceResult) return true;
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