using API.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface IDoctorServiceRepository
    {
        Task<IEnumerable<DoctorService>> GetDoctorServiceWithServiceByDoctorId(int doctorId);
        Task<List<DoctorService>> GetDoctorServiceByServiceId(int Id);
        Task<DoctorService> GetDoctorServiceById(int Id);
        void UpdateDoctorService(DoctorService doctorService);
        Task CreateDoctorServicesForService(int serviceId, List<int> doctorIdsWithSpeciality);
        Task CreateDoctorServicesForDoctor(int doctorId, List<int> serviceIdsWithSpeciality);
        Task<DoctorService> GetDoctorServiceWithServiceAndItemsById(int Id);
        void DeleteDoctorServices(List<DoctorService> servicesToRemove);
        Task<bool> SaveAllAsync();
    }
}