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
            _context.Services.Add(service);
        }

        public void UpdateService(Service service)
        {
            _context.Services.Update(service);
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
        public async Task CreateDoctorServicesForService(Service service)
        {
            var doctorIdsWithSpeciality = await _context.Users
                .Where(d => d.DoctorSpecialityId == service.ServiceSpecialityId)
                .Select(d => d.Id)
                .ToListAsync();

            var doctorServices = doctorIdsWithSpeciality.Select(doctorId => new DoctorService
            {
                DoctorId = doctorId,
                ServiceId = service.Id,
                DoctorPercentage = 50,
                HospitalPercentage = 50
            }).ToList();

            await _context.DoctorServices.AddRangeAsync(doctorServices);
        }
        public async Task UpdateDoctorServicesForService(Service service)
        {
            // Start a new transaction
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Fetch and delete existing DoctorServices for the service
                var doctorServicesToDelete = await _context.DoctorServices
                    .Where(ds => ds.ServiceId == service.Id)
                    .ToListAsync();

                _context.DoctorServices.RemoveRange(doctorServicesToDelete);

                await _context.SaveChangesAsync();

                // Create new DoctorServices for the service
                await CreateDoctorServicesForService(service);

                // Commit the transaction
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                // Rollback the transaction in case of any errors
                await transaction.RollbackAsync();
                throw; // Re-throw the exception to be handled by the calling code
            }
        }
    }
}