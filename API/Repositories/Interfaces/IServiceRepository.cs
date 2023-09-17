using API.Helpers;
using API.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        void AddService(Service service);
        void DeleteService(Service service);
        Task<Service> GetServiceById(int id);
        Task<bool> SaveAllAsync();
        Task<PagedList<Service>> GetServicesAsync(ServiceParams serviceParams);
        void CreateDoctorServicesForService(Service service);
    }
}