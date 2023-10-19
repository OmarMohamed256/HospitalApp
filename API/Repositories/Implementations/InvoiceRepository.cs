using API.Models.Entities;
using API.Repositories.Interfaces;
using Hospital.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _context;
        public InvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddInvoice(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
        }
        public void UpdateInvoice(Invoice invoice)
        {
            _context.Invoices.Update(invoice);
        }
        public void AddInvoiceDoctorService(InvoiceDoctorService invoiceDoctorService)
        {
            _context.InvoiceDoctorService.Add(invoiceDoctorService);
        }

        public void AddInvoiceDoctorServiceOrders(InvoiceDoctorServiceSupplyOrders invoiceDoctorServiceSupplyOrder)
        {
            _context.InvoiceDoctorServiceSupplyOrders.Add(invoiceDoctorServiceSupplyOrder);
        }

        public async Task<Invoice> GetInvoiceByIdAsync(int invoiceId)
        {
            return await _context.Invoices
            .Include(i => i.CustomItems)
            .Include(i => i.InvoiceDoctorService)
            .SingleOrDefaultAsync(i => i.Id == invoiceId);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}