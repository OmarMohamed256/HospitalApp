namespace API.Models.Entities
{
    public class CustomItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int? Units { get; set; }
        public int? InvoiceId { get; set; }
        public virtual Invoice? Invoice { get; set; }

    }
}