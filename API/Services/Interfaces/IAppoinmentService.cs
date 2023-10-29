using API.Helpers;
using API.Models.DTOS;

namespace API.Services.Interfaces
{
    public interface IAppoinmentService
    {
        Task<PagedList<AppointmentDto>> GetAppointmentsForPatientAsync(AppointmentParams appointmentParams, int patientId);
        Task<PagedList<AppointmentDto>> GetAppointmentsForDoctorAsync(AppointmentParams appointmentParams, int doctorId);
        Task<List<DateTime>> GetUpcomingAppointmentsDatesByDoctorIdAsync(int doctorId);
        Task<PagedList<AppointmentDto>> GetAppointmentsAsync(AppointmentParams appointmentParams);
        Task<AppointmentDto> GetAppointmentByIdAsync(int appointmentId);
        Task<AppointmentDto> CreateUpdateAppointmentAsync(AppointmentDto appointmentDto);
        Task DeleteAppointment(int appointmentId);
        Task<ICollection<AppointmentDto>> GetFirstTwoUpcomingAppointmentsForDoctorById(int doctorId);

    }
}