using API.Helpers;
using API.Repositories.Interfaces;
using Hospital.Data;
using HospitalApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations
{
    public class AppoinmentRepository : IAppoinmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppoinmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddAppointment(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
        }
        public void UpdateAppointment(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
        }
        public async Task<PagedList<Appointment>> GetAppointmentsAsync(AppointmentParams appointmentParams)
        {
            var query = _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .AsQueryable();

            if (appointmentParams.SpecialityId != null)
                query = query.Where(u => u.AppointmentSpecialityId == appointmentParams.SpecialityId);

            if(appointmentParams.AppointmentDateOfVisit != DateTime.MinValue)
                query = query.Where(a => EF.Functions.DateDiffDay(a.DateOfVisit, appointmentParams.AppointmentDateOfVisit) == 0);

            if (!string.IsNullOrEmpty(appointmentParams.Type)) query = query.Where(u => u.Type == appointmentParams.Type);

            query = (appointmentParams.OrderBy, appointmentParams.Order) switch
            {
                ("dateUpdated", "asc") => query.OrderBy(u => u.DateUpdated),
                ("dateUpdated", "desc") => query.OrderByDescending(u => u.DateUpdated),
                ("dateCreated", "asc") => query.OrderBy(u => u.DateCreated),
                ("dateCreated", "desc") => query.OrderByDescending(u => u.DateCreated),
                ("dateOfVisit", "asc") => query.OrderBy(u => u.DateOfVisit),
                ("dateOfVisit", "desc") => query.OrderByDescending(u => u.DateOfVisit),
                _ => query.OrderByDescending(u => u.DateCreated),
            };
            return await PagedList<Appointment>.CreateAsync(query, appointmentParams.PageNumber, appointmentParams.PageSize);
        }

        public async Task<PagedList<Appointment>> GetAppointmentsByPatientIdAsync(AppointmentParams appointmentParams, int patientId)
        {
            var query = _context.Appointments
            .Include(a => a.Doctor)
            .Where(p => p.PatientId == patientId)
            .AsQueryable();

            if (appointmentParams.SpecialityId != null)
                query = query.Where(u => u.AppointmentSpecialityId == appointmentParams.SpecialityId);

            if(appointmentParams.AppointmentDateOfVisit != DateTime.MinValue)
                query = query.Where(a => EF.Functions.DateDiffDay(a.DateOfVisit, appointmentParams.AppointmentDateOfVisit) == 0);
                
            if (!string.IsNullOrEmpty(appointmentParams.Type)) query = query.Where(u => u.Type == appointmentParams.Type);

            query = (appointmentParams.OrderBy, appointmentParams.Order) switch
            {
                ("dateUpdated", "asc") => query.OrderBy(u => u.DateUpdated),
                ("dateUpdated", "desc") => query.OrderByDescending(u => u.DateUpdated),
                ("dateCreated", "asc") => query.OrderBy(u => u.DateCreated),
                ("dateCreated", "desc") => query.OrderByDescending(u => u.DateCreated),
                ("dateOfVisit", "asc") => query.OrderBy(u => u.DateOfVisit),
                ("dateOfVisit", "desc") => query.OrderByDescending(u => u.DateOfVisit),
                _ => query.OrderByDescending(u => u.DateCreated),
            };

            return await PagedList<Appointment>.CreateAsync(query, appointmentParams.PageNumber, appointmentParams.PageSize);
        }

        public async Task<List<DateTime>> GetUpcomingAppointmentsDatesByDoctorIdAsync(int doctorId)
        {
            var upcomingDates = await _context.Appointments
                .AsNoTracking()
                .Where(a => a.DoctorId == doctorId && a.DateOfVisit >= DateTime.Now)
                .Select(a => a.DateOfVisit) // Selecting only DateOfVisit
                .ToListAsync();
            return upcomingDates;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Appointment> GetAppointmentsForUserByDateOfVisit(DateTime dateOfVisit)
        {
            return await _context.Appointments
            .AsNoTracking()
            .SingleOrDefaultAsync(a => a.DateOfVisit == dateOfVisit);
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int appointmentId)
        {
            return await _context.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .FirstOrDefaultAsync(a => a.Id == appointmentId);
        }

        public int UpdateAppointmentStatus(int appointmentId, string status)
        {
            return _context.Appointments
               .Where(u => u.Id == appointmentId)
               .ExecuteUpdate(b =>
                   b.SetProperty(u => u.Status, status)
               );
        }
    }
}