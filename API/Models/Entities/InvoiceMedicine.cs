namespace API.Models.Entities
{
    public class InvoiceMedicine
    {
        public int InvoiceId { get; set; }
        public int MedicineId { get; set; }
        public string DosageAmount { get; set; }
        public string Duration { get; set; }
        public string Frequency { get; set; }
        public string? Note { get; set; }
        // Navigation properties
        public virtual Invoice Invoice { get; set; }
        public virtual Medicine Medicine { get; set; }
    }
}