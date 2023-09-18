using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface IDoctorServiceRepository
    {
        Task<IEnumerable<DoctorService>> GetDoctorServiceByDoctorId(int doctorId);
        Task<DoctorService> GetDoctorServiceById(int Id);
        void UpdateDoctorService(DoctorService doctorService);
        Task CreateDoctorServicesForService(Service service);
        Task UpdateDoctorServicesForService(Service service);
        Task<bool> SaveAllAsync();
    }
}