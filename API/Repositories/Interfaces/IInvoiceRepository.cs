using API.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        void AddInvoice(Invoice invoice);
        void UpdateInvoice(Invoice invoice);
        void RemoveInvoiceDoctorServiceSupplyOrder(InvoiceDoctorServiceSupplyOrders invoiceDoctorServiceSupplyOrder);
        void AddInvoiceDoctorService(InvoiceDoctorService invoiceDoctorService);
        void AddInvoiceDoctorServiceOrders(InvoiceDoctorServiceSupplyOrders invoiceDoctorServiceSupplyOrder);
        Task<Invoice> GetInvoiceByIdAsync(int invoiceId);
        Task<Invoice> GetInvoiceByIdWithPropertiesAsync(int invoiceId);
        Task<bool> SaveAllAsync();
    }
}