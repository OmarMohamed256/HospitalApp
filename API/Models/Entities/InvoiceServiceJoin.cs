namespace API.Models.Entities
{
    public class InvoiceServiceJoin
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int InvoiceId { get; set; }
        public int Quantity { get; set; }
        public virtual Service Service { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}