namespace API.Helpers
{
    public class AppointmentParams : PaginationParams
    {
        public string OrderBy { get; set; } = "dateCreated";
        public string Order { get; set; } = "desc";
        public int? SpecialityId { get; set; } = null;
        public string? Type { get; set; } = "";

    }
}