using API.Helpers;
using API.Models.DTOS;

namespace API.Services.Interfaces
{
    public interface IAppoinmentService
    {
        Task<PagedList<AppointmentDto>> GetAppointmentsForUser(AppointmentParams appointmentParams, int patientId);
    }
}