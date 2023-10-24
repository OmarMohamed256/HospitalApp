namespace API.Models.DTOS.InvoiceDtos
{
    public class InvoiceMedicineDto
    {
        public int InvoiceId { get; set; }
        public int MedicineId { get; set; }
        public string DosageAmount { get; set; }
        public string Duration { get; set; }
        public string Frequency { get; set; }
        public string? Note { get; set; }
        public MedicineDto? Medicine { get; set; }
    }
}