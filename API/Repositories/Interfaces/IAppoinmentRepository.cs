using API.Helpers;
using HospitalApp.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface IAppoinmentRepository
    {
        void AddAppointment(Appointment appointment);
        void UpdateAppointment(Appointment appointment);
        Task<PagedList<Appointment>> GetAppointmentsByPatientIdAsync(AppointmentParams appointmentParams, int patientId);
        Task<PagedList<Appointment>> GetAppointmentsAsync(AppointmentParams appointmentParams);
        Task<bool> SaveAllAsync();

    }
}