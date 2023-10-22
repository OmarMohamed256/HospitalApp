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

        public async Task<PagedList<Clinic>> GetAllClinicsWithUpComingAppointmentsAsync(ClinicParams clinicParams)
        {
            IQueryable<Clinic> query;

            if (clinicParams.IncludeUpcomingAppointments)
            {
                query = _context.Clinics
                    .Include(r => r.Doctor)
                        .ThenInclude(a => a.BookedWithAppointments
                            .Where(a => a.DateOfVisit > DateTime.Today
                                && (clinicParams.AppointmentDateOfVisit == DateTime.MinValue
                                    || EF.Functions.DateDiffDay(a.DateOfVisit, clinicParams.AppointmentDateOfVisit) == 0))
                            .OrderBy(a => a.DateOfVisit))
                    .AsQueryable();

            }
            else
            {
                query = _context.Clinics
                    .Include(r => r.Doctor)
                    .AsQueryable();
            }
            if(clinicParams.ClinicSpecialityId != null)
                query = query.Where(u => u.ClinicSpecialityId == clinicParams.ClinicSpecialityId);

            return await PagedList<Clinic>.CreateAsync(query, clinicParams.PageNumber, clinicParams.PageSize);
        }

        public async Task<Clinic> GetClinicByDoctorId(int doctorId)
        {
            return await _context.Clinics
                .Where(r => r.DoctorId == doctorId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<Clinic> GetClinicById(int clinicId)
        {
            return await _context.Clinics
                .Where(r => r.Id == clinicId)
                .AsNoTracking()
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