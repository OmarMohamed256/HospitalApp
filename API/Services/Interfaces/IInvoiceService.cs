using API.Models.DTOS;

namespace API.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task <InvoiceDto> CreateInvoiceAsync(CreateInvoiceDto invoiceDto);
        Task <InvoiceDto> UpdateInvoiceAsync(CreateInvoiceDto invoiceDto);
        Task<InvoiceDto> GetInvoiceByIdAsync(int invoiceId);
        Task<ICollection<MedicineDto>> GetMedicinesByInvoiceId(int invoiceId);
    }
}