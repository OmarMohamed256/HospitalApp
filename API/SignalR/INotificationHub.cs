
namespace API.SignalR
{
    public interface INotificationHub
    {
        Task SendAppointmentStatusChange(AppointmentStatusSignalR appointmentStatus);
    }
}