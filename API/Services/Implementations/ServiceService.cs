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
            var service = _mapper.Map<Service>(createServiceDto);
            service.DoctorServices = await PopulateServiceDoctorServices(service);
            _serviceRepository.AddService(service);
            bool saveService = await _serviceRepository.SaveAllAsync();
            if (saveService) return _mapper.Map<CreateServiceDTO>(service);
            throw new Exception("Failed to add service");
        }
        public async Task<CreateServiceDTO> UpdateServiceAsync(CreateServiceDTO serviceDto)
        {
            // update service
            var newService = _mapper.Map<Service>(serviceDto);
            var service = await _serviceRepository
                .GetServiceById(serviceDto.Id) ?? throw new BadRequestException("Service does not exist");
            service.Name = serviceDto.Name;
            service.TotalPrice = serviceDto.TotalPrice;
            service.ServiceSpecialityId = serviceDto.ServiceSpecialityId;
            service.ServiceInventoryItems = newService.ServiceInventoryItems;
            service.DoctorServices = await PopulateServiceDoctorServices(newService);
            _serviceRepository.UpdateService(service);
            bool saveService = await _serviceRepository.SaveAllAsync();
            if (!saveService) throw new Exception("Failed to update service");
            return _mapper.Map<CreateServiceDTO>(service);
        }
        private async Task<List<int>> GetDoctorIdsListBySpecialityId(int specilaityId)
        {
            return await _doctorRepository.GetDoctorsIdsBySpecialityId(specilaityId);
        }

        private async Task<ICollection<DoctorService>> PopulateServiceDoctorServices(Service service)
        {
            ICollection<DoctorService> doctorServices = new List<DoctorService>();
            var doctorsWithSpecialityId = await GetDoctorIdsListBySpecialityId(service.ServiceSpecialityId);
            if (!doctorsWithSpecialityId.Any()) return doctorServices;
            doctorServices = doctorsWithSpecialityId.Select(doctorId => new DoctorService
            {
                DoctorId = doctorId,
                ServiceId = service.Id,
                DoctorPercentage = 50,
                HospitalPercentage = 50
            }).ToList();
            return doctorServices;
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