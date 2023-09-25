using API.Helpers;
using API.Models.Entities;
using API.Repositories.Interfaces;
using Hospital.Data;

namespace API.Repositories.Implementations
{
    public class SupplyOrderRepository : ISupplyOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public SupplyOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddSupplyOrder(SupplyOrder supplyOrder)
        {
            _context.SupplyOrders.Add(supplyOrder);
        }
        public void UpdateSupplyOrder(SupplyOrder supplyOrder)
        {
            _context.SupplyOrders.Update(supplyOrder);
        }

        public async Task<PagedList<SupplyOrder>> GetSupplyOrdersAsync(SupplyOrderParams supplyOrderParams)
        {
            var query = _context.SupplyOrders.AsQueryable();

            if (!string.IsNullOrEmpty(supplyOrderParams.SearchTerm)) query = query
                .Where(u => u.ItemName.Contains(supplyOrderParams.SearchTerm) || u.Note.Contains(supplyOrderParams.SearchTerm));

            if (supplyOrderParams.InventoryItemId != null)
                query = query.Where(u => u.InventoryItemId == supplyOrderParams.InventoryItemId);

            query = (supplyOrderParams.OrderBy, supplyOrderParams.Order) switch
            {
                ("dateUpdated", "asc") => query.OrderBy(u => u.DateUpdated),
                ("dateUpdated", "desc") => query.OrderByDescending(u => u.DateUpdated),
                ("dateCreated", "asc") => query.OrderBy(u => u.DateCreated),
                ("dateCreated", "desc") => query.OrderByDescending(u => u.DateCreated),
                ("dateOfExpiry", "asc") => query.OrderBy(u => u.ExpiryDate),
                ("dateOfExpiry", "desc") => query.OrderByDescending(u => u.ExpiryDate),
                _ => query.OrderByDescending(u => u.DateCreated),
            };
            return await PagedList<SupplyOrder>.CreateAsync(query, supplyOrderParams.PageNumber, supplyOrderParams.PageSize);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}