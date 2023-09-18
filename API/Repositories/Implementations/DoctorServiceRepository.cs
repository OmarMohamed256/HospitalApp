using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<DoctorService>> GetDoctorServiceByDoctorId(int doctorId)
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