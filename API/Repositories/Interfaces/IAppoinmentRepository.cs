using API.Helpers;
using HospitalApp.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface IAppoinmentRepository
    {
        void AddAppointment(Appointment appointment);
        Task<PagedList<Appointment>> GetAppointmentsForUser(AppointmentParams appointmentParams, int patientId);
    }
}