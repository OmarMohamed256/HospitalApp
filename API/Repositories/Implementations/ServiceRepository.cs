using API.Helpers;
using API.Models.Entities;
using API.Repositories.Interfaces;
using Hospital.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _context;
        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddService(Service service)
        {
            var existingService = _context.Services.AsNoTracking().FirstOrDefault(s => s.Id == service.Id);

            if (existingService != null) _context.Services.Update(service);
            else _context.Services.Add(service);
        }

        public void DeleteService(Service service)
        {
            _context.Services.Remove(service);
        }

        public async Task<Service> GetServiceById(int id)
        {
            return await _context.Services
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedList<Service>> GetServicesAsync(ServiceParams serviceParams)
        {
            var query = _context.Services.AsQueryable();
            if (serviceParams.SpecialityId != null)
                query = query.Where(u => u.ServiceSpecialityId == serviceParams.SpecialityId);
            if (!string.IsNullOrEmpty(serviceParams.SearchTerm)) query = query.Where(u => u.Name.Contains(serviceParams.SearchTerm));


            return await PagedList<Service>.CreateAsync(query, serviceParams.PageNumber, serviceParams.PageSize);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public void CreateDoctorServicesForService(Service service)
        {
            var doctorSpecialityId = service.ServiceSpecialityId;
            var doctorsWithSpeciality = _context.Users
                .Where(d => d.DoctorSpecialityId == doctorSpecialityId)
                .ToList();

            foreach (var doctor in doctorsWithSpeciality)
            {
                var doctorService = new DoctorService
                {
                    DoctorId = doctor.Id,
                    ServiceId = service.Id,
                    DoctorPercentage = 50,
                    HospitalPercentage = 50
                };

                _context.DoctorServices.Add(doctorService);
            }
        }
    }
}