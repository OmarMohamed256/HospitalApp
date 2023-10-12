namespace API.Helpers
{
    public class RoomParams : PaginationParams
    {
        public bool IncludeUpcomingAppointments { get; set; } = false;
        public DateTime AppointmentDateOfVisit { get; set; } = DateTime.MinValue;
        public int? RoomSpecialityId { get; set; } = null;
    }
}