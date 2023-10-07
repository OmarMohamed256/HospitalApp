namespace API.Helpers
{
    public class RoomParams : PaginationParams
    {
        public bool IncludeUpcomingAppointments { get; set; } = false;
    }
}