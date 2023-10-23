namespace API.Models.DTOS.InvoiceDtos
{
    public class InvoiceMedicineDto
    {
        public int InvoiceId { get; set; }
        public int MedicineId { get; set; }
        public MedicineDto? Medicine { get; set; }
    }
}