namespace API.Helpers
{
    public class InventoryItemParams : PaginationParams
    {
        public int? SpecialityId { get; set; } = null;
        public string? SearchTerm { get; set; } = "";
    }
}