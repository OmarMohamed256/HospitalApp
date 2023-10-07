
namespace API.SignalR
{
    public interface IAppointmentHub
    {
        public Task SendAppointmentFinalized(int appointmentId);
    }
}