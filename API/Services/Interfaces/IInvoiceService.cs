using API.Models.DTOS;

namespace API.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task <InvoiceDto> CreateInvoiceAsync(CreateInvoiceDto invoiceDto);
        Task<InvoiceDto> GetInvoiceByIdAsync(int invoiceId);

    }
}