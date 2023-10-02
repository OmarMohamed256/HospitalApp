using API.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        void AddInvoice(Invoice invoice);
        void AddInvoiceDoctorService(InvoiceDoctorService invoiceDoctorService);
        void AddInvoiceDoctorServiceOrders(InvoiceDoctorServiceSupplyOrders invoiceDoctorServiceSupplyOrder);
        Task<bool> SaveAllAsync();
    }
}