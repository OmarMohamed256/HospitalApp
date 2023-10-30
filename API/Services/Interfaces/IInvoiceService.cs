using API.Models.DTOS;

namespace API.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task <InvoiceDto> CreateInvoiceAsync(CreateInvoiceDto invoiceDto);
        Task <InvoiceDto> UpdateInvoiceByUserRoleAsync(CreateInvoiceDto invoiceDto, string userRole);
        Task<InvoiceDto> GetInvoiceByIdAsync(int invoiceId);
        Task<ICollection<MedicineDto>> GetMedicinesByInvoiceId(int invoiceId);
        Task<decimal> UpdateInvoiceDebt(int invoiceId, decimal totalPaid);
    }
}