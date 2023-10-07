namespace API.Models.DTOS
{
    public class CustomItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Units { get; set; }
        public decimal TotalPrice { get; set; }
        public int InvoiceId { get; set; }
    }
}