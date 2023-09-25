using API.Helpers;
using API.Repositories.Interfaces;
using Hospital.Data;
using HospitalApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using webapi.Entities;

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
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}