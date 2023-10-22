namespace API.Helpers
{
    public class ClinicParams : PaginationParams
    {
        public bool IncludeUpcomingAppointments { get; set; } = false;
        public DateTime AppointmentDateOfVisit { get; set; } = DateTime.MinValue;
        public int? ClinicSpecialityId { get; set; } = null;
    }
}