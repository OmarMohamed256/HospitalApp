namespace API.Helpers.Params
{
    public class MedicineParams : PaginationParams
    {
        public string? SearchTerm { get; set; } = "";
    }
}