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
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var service = _mapper.Map<Service>(serviceDto);
                _serviceRepository.AddService(service);
                bool saveService = await _serviceRepository.SaveAllAsync();
                if (saveService)
                {
                    var serviceDtoNew = await HandleDoctorServicesAsync(service, serviceDto);
                    scope.Complete();
                    return serviceDtoNew;
                }
                else
                {
                    throw new ApiException(HttpStatusCode.InternalServerError, "Failed to add service");
                }
            }
            catch (Exception)
            {
                scope.Dispose();
                throw;
            }
        }

        public async Task<ServiceDto> UpdateServiceAsync(ServiceDto serviceDto)
        {
            // Start a new transaction
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var service = await _serviceRepository.GetServiceById(serviceDto.Id) ?? throw new ApiException(HttpStatusCode.BadRequest, "Service does not exist");
                service = _mapper.Map<Service>(serviceDto);
                _serviceRepository.UpdateService(service);

                // add delete DoctorServices if speciality changed
                if(service.ServiceSpecialityId == serviceDto.ServiceSpecialityId)
                {
                    var result = await _serviceRepository.SaveAllAsync();
                    scope.Complete();
                    if (result) return _mapper.Map<ServiceDto>(service);
                    else throw new ApiException(HttpStatusCode.InternalServerError, "Failed to update service");
                }

                var servicesToDelete = await _doctorServiceRepository.GetDoctorServiceByServiceId(service.Id);

                if (servicesToDelete == null || !servicesToDelete.Any())
                {
                    var result = await HandleDoctorServicesAsync(service, serviceDto);
                    scope.Complete();
                    return result;
                }

                // Call UpdateDoctorServicesForService after updating the service
                bool deleteDoctorsResult = await HandleDeleteDoctorServicesAsync(servicesToDelete);
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
                throw;
            }
        }
        
        private async Task<bool> HandleDeleteDoctorServicesAsync(List<DoctorService>? servicesToDelete)
        {
            // Call UpdateDoctorServicesForService after updating the service
            _doctorServiceRepository.DeleteDoctorServices(servicesToDelete);
            bool deleteDoctorsResult = await _serviceRepository.SaveAllAsync();
            return deleteDoctorsResult;
        }

        private async Task<ServiceDto> HandleDoctorServicesAsync(Service service, ServiceDto serviceDto)
        {
            var doctorsWithSpecialityId = await _doctorRepository.GetDoctorsIdsBySpecialityId(service.ServiceSpecialityId);
            if (!doctorsWithSpecialityId.Any())
            {
                var result = await _serviceRepository.SaveAllAsync();
                if (result) return _mapper.Map<ServiceDto>(service);
                else throw new ApiException(HttpStatusCode.InternalServerError, "Failed to add service");
            }
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