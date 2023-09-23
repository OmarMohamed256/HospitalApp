using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.Entities;
using webapi.Entities;

namespace API.Repositories.Interfaces
{
    public interface IDoctorServiceRepository
    {
        Task<IEnumerable<DoctorService>> GetDoctorServiceByDoctorId(int doctorId);
        Task<List<DoctorService>> GetDoctorServiceByServiceId(int Id);
        Task<DoctorService> GetDoctorServiceById(int Id);
        void UpdateDoctorService(DoctorService doctorService);
        Task CreateDoctorServicesForService(int serviceId, List<int> doctorIdsWithSpeciality);
        Task CreateDoctorServicesForDoctor(int doctorId, List<int> serviceIdsWithSpeciality);
        // Task UpdateDoctorServicesForService(Service service);
        void DeleteDoctorServices(List<DoctorService> servicesToRemove);
        Task<bool> SaveAllAsync();
    }
}