namespace API.Models.Entities
{
    public class InvoiceMedicine
    {
        public int InvoiceId { get; set; }
        public int MedicineId { get; set; }
        // Navigation properties
        public virtual Invoice Invoice { get; set; }
        public virtual Medicine Medicine { get; set; }
    }
}