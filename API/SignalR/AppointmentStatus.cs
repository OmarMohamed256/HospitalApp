namespace API.SignalR
{
    public class AppointmentStatus
    {
        public int AppointmentId { get; set; }
        public string Status { get; set; } = "finalized";
    }
}