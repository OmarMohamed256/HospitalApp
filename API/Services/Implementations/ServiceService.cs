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
        private readonly ISupplyOrderRepository _supplyOrderRepository;
        public ServiceService(IServiceRepository serviceRepository, IMapper mapper,
        IDoctorServiceRepository doctorServiceRepository, IUserRepository doctorRepository
        , ISupplyOrderRepository supplyOrderRepository)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _doctorServiceRepository = doctorServiceRepository;
            _doctorRepository = doctorRepository;
            _supplyOrderRepository = supplyOrderRepository;
        }
        public async Task<CreateServiceDTO> CreateServiceAsync(CreateServiceDTO createServiceDto)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var service = _mapper.Map<Service>(createServiceDto);
                _serviceRepository.AddService(service);
                bool saveService = await _serviceRepository.SaveAllAsync();
                if (saveService)
                {
                    // Handle adding doctor services
                    var serviceDtoNew = await HandleCreateDoctorServicesAsync(service);
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

        private async Task<CreateServiceDTO> HandleCreateDoctorServicesAsync(Service service)
        {
            var doctorsWithSpecialityId = await _doctorRepository.GetDoctorsIdsBySpecialityId(service.ServiceSpecialityId);
            if (!doctorsWithSpecialityId.Any()) return _mapper.Map<CreateServiceDTO>(service);
            else
            {
                await _doctorServiceRepository.CreateDoctorServicesForService(service.Id, doctorsWithSpecialityId);
                bool createDoctorServicesResult = await _serviceRepository.SaveAllAsync();
                if (createDoctorServicesResult) return _mapper.Map<CreateServiceDTO>(service);
            }
            throw new ApiException(HttpStatusCode.InternalServerError, "Failed to add doctor services");
        }

        private async Task<bool> HandleDeleteDoctorServicesAsync(List<DoctorService>? servicesToDelete)
        {
            // Call UpdateDoctorServicesForService after updating the service
            _doctorServiceRepository.DeleteDoctorServices(servicesToDelete);
            bool deleteDoctorsResult = await _serviceRepository.SaveAllAsync();
            return deleteDoctorsResult;
        }
        public async Task<CreateServiceDTO> UpdateServiceAsync(CreateServiceDTO serviceDto)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var service = await _serviceRepository.GetServiceById(serviceDto.Id) ?? throw new ApiException(HttpStatusCode.BadRequest, "Service does not exist");
                var serviceId = service.Id;
                UpdateServiceInventoryItems(service, serviceDto);

                service.Name = serviceDto.Name;
                service.TotalPrice = serviceDto.TotalPrice;
                service.ServiceSpecialityId = serviceDto.ServiceSpecialityId;

                _serviceRepository.UpdateService(service);
                bool saveService = await _serviceRepository.SaveAllAsync();
                if (!saveService) throw new ApiException(HttpStatusCode.InternalServerError, "Failed to update service");

                // Add Delete DoctorServices if speciality changed
                if (service.ServiceSpecialityId == serviceId)
                {
                    scope.Complete();
                    return _mapper.Map<CreateServiceDTO>(service);
                }

                var servicesToDelete = await _doctorServiceRepository.GetDoctorServiceByServiceId(service.Id);
                if (servicesToDelete == null || !servicesToDelete.Any())
                {
                    var result = await HandleCreateDoctorServicesAsync(service);
                    scope.Complete();
                    return result;
                }

                bool deleteDoctorsResult = await HandleDeleteDoctorServicesAsync(servicesToDelete);
                if (!deleteDoctorsResult) throw new ApiException(HttpStatusCode.InternalServerError, "Failed to delete doctor service");

                var addDoctorServiceresult = await HandleCreateDoctorServicesAsync(service);
                scope.Complete();
                return addDoctorServiceresult;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> DeleteServiceAsync(int id)
        {
            var service = await _serviceRepository.GetServiceById(id) ?? throw new ApiException(HttpStatusCode.NotFound, "Service Does not Exist");
            _serviceRepository.DeleteService(service);
            bool deleteServiceResult = await _serviceRepository.SaveAllAsync();
            if (deleteServiceResult) return true;
            return false;
        }

        private void UpdateServiceInventoryItems(Service service, CreateServiceDTO serviceDto)
        {
            service.ServiceInventoryItems ??= new List<ServiceInventoryItem>();

            if (service.ServiceInventoryItems != null && service.ServiceInventoryItems.Any())
            {
                var updatedInventoryItemIds = serviceDto.ServiceInventoryItems.Select(i => i.InventoryItemId).ToList();
                var itemsToRemove = service.ServiceInventoryItems
                       .Where(item => !updatedInventoryItemIds.Contains(item.InventoryItemId))
                       .ToList();
                _serviceRepository.DeleteServiceInventoryItemsRangeAsync(itemsToRemove);
            }

            if (serviceDto.ServiceInventoryItems != null)
            {
                foreach (var inventoryItemDto in serviceDto.ServiceInventoryItems)
                {
                    var existingInventoryItem = service.ServiceInventoryItems
                        .FirstOrDefault(item => item.InventoryItemId == inventoryItemDto.InventoryItemId);

                    if (existingInventoryItem != null) existingInventoryItem.QuantityNeeded = inventoryItemDto.QuantityNeeded;
                    else
                    {
                        // Create and add new item
                        var newInventoryItem = new ServiceInventoryItem
                        {
                            InventoryItemId = inventoryItemDto.InventoryItemId,
                            QuantityNeeded = inventoryItemDto.QuantityNeeded
                        };
                        service.ServiceInventoryItems.Add(newInventoryItem);
                    }
                }
            }
        }

        public async Task<PagedList<ServiceDto>> GetServicesAsync(ServiceParams serviceParams)
        {
            var services = await _serviceRepository.GetServicesAsync(serviceParams);
            var servicesDtos = _mapper.Map<IEnumerable<ServiceDto>>(services);
            return new PagedList<ServiceDto>(servicesDtos, services.TotalCount, services.CurrentPage, services.PageSize);
        }

        public async Task<ICollection<ServiceInventoryItemDto>> GetServiceInventoryItemsByServiceId(int serviceId)
        {
            var servinceInventoryITems = await _serviceRepository.GetServiceInventoryItemsByServiceIdAsync(serviceId);
            return _mapper.Map<ICollection<ServiceInventoryItemDto>>(servinceInventoryITems);
        }

        public async Task<decimal> GetServiceDisposablesPriceAsync(int serviceId, int serviceQuantity)
        {
            var servinceInventoryItems = await _serviceRepository.GetServiceInventoryItemsByServiceIdAsync(serviceId);
            if (servinceInventoryItems.Count == 0) return 0;
            decimal TotalDisposablesPrice = 0;
            foreach (var item in servinceInventoryItems)
            {
                TotalDisposablesPrice += await GetDisposablePriceForAnInventoryItem(item.InventoryItem, serviceQuantity * item.QuantityNeeded);
            }
            return TotalDisposablesPrice;
        }

        private async Task<decimal> GetDisposablePriceForAnInventoryItem(InventoryItem inventoryItem, int quantityNeeded)
        {
            var consumableSupplyOrders = await _supplyOrderRepository.GetConsumableSupplyOrdersByInventoryItemId(inventoryItem.Id);
            int totalQuantity = consumableSupplyOrders.Sum(order => order.Quantity);
            if (totalQuantity < quantityNeeded) throw new Exception("Not enough supply orders to fulfill quantity needed for item: " + inventoryItem.Name);
            decimal totalItemDisposablePrice = 0;
            foreach (var supplyOrder in consumableSupplyOrders)
            {
                if (quantityNeeded > 0)
                {
                    int quantityToConsume = Math.Min(quantityNeeded, supplyOrder.Quantity);
                    supplyOrder.Quantity -= quantityToConsume;
                    quantityNeeded -= quantityToConsume;
                    totalItemDisposablePrice += supplyOrder.ItemPrice * quantityToConsume;
                }
                else
                {
                    break;
                }
            }
            return totalItemDisposablePrice;
        }
    }
}