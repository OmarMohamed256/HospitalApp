using API.Helpers;
using HospitalApp.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface IAppoinmentRepository
    {
        void AddAppointment(Appointment appointment);
        void UpdateAppointment(Appointment appointment);
        Task<PagedList<Appointment>> GetAppointmentsByPatientIdAsync(AppointmentParams appointmentParams, int patientId);
        Task<List<DateTime>> GetUpcomingAppointmentsDatesByDoctorIdAsync(int doctorId);
        Task<PagedList<Appointment>> GetAppointmentsAsync(AppointmentParams appointmentParams);
        Task<Appointment> GetAppointmentsForUserByDateOfVisit(DateTime dateOfVisit);
        Task<bool> SaveAllAsync();

    }
}