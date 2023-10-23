namespace API.Models.Entities
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<InvoiceMedicine>? InvoiceMedicines { get; set; }
    }
}