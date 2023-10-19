using API.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        void AddInvoice(Invoice invoice);
        void UpdateInvoice(Invoice invoice);
        void AddInvoiceDoctorService(InvoiceDoctorService invoiceDoctorService);
        void AddInvoiceDoctorServiceOrders(InvoiceDoctorServiceSupplyOrders invoiceDoctorServiceSupplyOrder);
        Task<Invoice> GetInvoiceByIdAsync(int invoiceId);
        Task<bool> SaveAllAsync();
    }
}