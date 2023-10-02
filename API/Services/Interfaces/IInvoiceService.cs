using API.Models.DTOS;

namespace API.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task <InvoiceDto> CreateInvoiceAsync(InvoiceDto invoiceDto);
    }
}