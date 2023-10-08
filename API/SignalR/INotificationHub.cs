
namespace API.SignalR
{
    public interface INotificationHub
    {
        public Task SendAppointmentFinalized(int appointmentId);
    }
}