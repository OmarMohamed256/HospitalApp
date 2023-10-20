namespace API.SignalR
{
    public class AppointmentStatusSignalR
    {
        public int AppointmentId { get; set; }
        public string Status { get; set; } = "finalized";
    }
}