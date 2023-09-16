namespace API.Helpers
{
    public class ServiceParams : PaginationParams
    {
        public int? SpecialityId { get; set; } = null;
        public string? SearchTerm { get; set; } = "";
    }
}