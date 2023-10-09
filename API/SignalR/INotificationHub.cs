
namespace API.SignalR
{
    public interface INotificationHub
    {
        Task SendAppointmentFinalized(AppointmentStatus appointmentStatus);
    }
}