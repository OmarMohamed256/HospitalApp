namespace API.Helpers
{
    public class OrderParams : PaginationParams
    {
        public string OrderBy { get; set; } = "dateCreated";
        public string Order { get; set; } = "desc";
        public string SearchTerm { get; set; } = "";
        public int? InventoryItemId { get; set; } = null;
    }
}