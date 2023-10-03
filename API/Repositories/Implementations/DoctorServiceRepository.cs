using API.Models.Entities;
using API.Repositories.Interfaces;
using Hospital.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations
{
    public class DoctorServiceRepository : IDoctorServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DoctorService>> GetDoctorServiceWithServiceByDoctorId(int doctorId)
        {
            return await _context.DoctorServices
                .Where(ds => ds.DoctorId == doctorId)
                .Include(ds => ds.Service)
                .ToListAsync();
        }

        public void UpdateDoctorService(DoctorService doctorService)
        {
            _context.DoctorServices.Update(doctorService);
        }

        public async Task CreateDoctorServicesForService(int serviceId, List<int> doctorIdsWithSpeciality)
        {
            var doctorServices = doctorIdsWithSpeciality.Select(doctorId => new DoctorService
            {
                DoctorId = doctorId,
                ServiceId = serviceId,
                DoctorPercentage = 50,
                HospitalPercentage = 50
            }).ToList();

            await _context.DoctorServices.AddRangeAsync(doctorServices);
        }
        public async Task CreateDoctorServicesForDoctor(int doctorId, List<int> serviceIdsWithSpeciality)
        {
            var doctorServices = serviceIdsWithSpeciality.Select(serviceId => new DoctorService
            {
                DoctorId = doctorId,
                ServiceId = serviceId,
                DoctorPercentage = 50,
                HospitalPercentage = 50
            }).ToList();

            await _context.DoctorServices.AddRangeAsync(doctorServices);
        }

        public async Task<List<DoctorService>> GetDoctorServiceByServiceId(int Id)
        {
            return await _context.DoctorServices
                .Where(ds => ds.ServiceId == Id)
                .ToListAsync();
        }

        public void DeleteDoctorServices(List<DoctorService> servicesToRemove)
        {
            _context.DoctorServices.RemoveRange(servicesToRemove);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<DoctorService> GetDoctorServiceById(int Id)
        {
            return await _context.DoctorServices
                .AsNoTracking()
                .FirstOrDefaultAsync(ds => ds.Id == Id);
        }

        public async Task<DoctorService> GetDoctorServiceWithServiceAndItemsById(int Id)
        {
            return await _context.DoctorServices
                .AsNoTracking()
                .Include(ds => ds.Service)
                    .ThenInclude(s => s.ServiceInventoryItems)
                .FirstOrDefaultAsync(ds => ds.Id == Id);
        }
    }
}