using API.Helpers;
using API.Models.Entities;
using API.Repositories.Interfaces;
using Hospital.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations
{
    public class ClinicRepository : IClinicRepository
    {

        private readonly ApplicationDbContext _context;
        public ClinicRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddClinic(Clinic clinic)
        {
            _context.Clinics.Add(clinic);
        }

        public void DeleteClinic(Clinic clinic)
        {
            _context.Clinics.Remove(clinic);
        }
        public async Task<PagedList<Clinic>> GetClinicsWithFirstTwoUpcomingAppointmentsAsync(ClinicParams clinicParams)
        {
            var query = _context.Clinics
                .Include(c => c.ClinicDoctors)
                    .ThenInclude(cd => cd.Doctor)
                        .ThenInclude(d => d.BookedWithAppointments
                        .Where(appointment => appointment.DateOfVisit > DateTime.Now)
                        .OrderBy(appointment => appointment.DateOfVisit)
                        .Take(2))
                .AsQueryable();
            return await PagedList<Clinic>.CreateAsync(query, clinicParams.PageNumber, clinicParams.PageSize);
        }
        public async Task<PagedList<Clinic>> GetClinics(ClinicParams clinicParams)
        {
            var query = _context.Clinics
            .Include(c => c.ClinicDoctors)
            .ThenInclude(cd => cd.Doctor)
            .AsQueryable();
            return await PagedList<Clinic>.CreateAsync(query, clinicParams.PageNumber, clinicParams.PageSize);
        }

        public async Task<Clinic> GetClinicById(int clinicId)
        {
            return await _context.Clinics
                .Include(c => c.ClinicDoctors)
                .Where(c => c.Id == clinicId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateClinic(Clinic clinic)
        {
            _context.Clinics.Update(clinic);
        }

    }
}