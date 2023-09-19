namespace API.Helpers
{
    public class UserParams : PaginationParams
    {
        public string OrderBy { get; set; } = "date";
        public string Order { get; set; } = "asc";
        public string? SearchTerm  { get; set; } = "";
        public string Gender  { get; set; } = "";
        public int? DoctorSpecialityId  { get; set; } = null;
        public string? RoleName { get; set; }
    }
}
