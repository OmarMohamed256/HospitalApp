using API.Helpers;
using API.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        void AddService(Service service);
        void UpdateService(Service service);
        void DeleteService(Service service);
        Task AddServiceInventoryItemsRangeAsync(ICollection<ServiceInventoryItem> ServiceInventoryItems);
        void DeleteServiceInventoryItemsRangeAsync(ICollection<ServiceInventoryItem> ServiceInventoryItems);
        Task <ICollection<ServiceInventoryItem>> GetServiceInventoryItemsByServiceIdAsync(int serviceId);
        Task<Service> GetServiceById(int id);
        Task<bool> SaveAllAsync();
        Task<PagedList<Service>> GetServicesAsync(ServiceParams serviceParams);
        Task<List<int>> GetServicesIdsBySpecialityId(int specialityId);
    }
}