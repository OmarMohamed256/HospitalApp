using API.Helpers;
using API.Models.Entities;
using API.Repositories.Interfaces;
using Hospital.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations
{
    public class SellOrderRepository : ISellOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public SellOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddSellOrder(SellOrder sellOrder)
        {
            _context.SellOrders.Add(sellOrder);
        }
        public void UpdateSellOrder(SellOrder sellOrder)
        {
            _context.SellOrders.Update(sellOrder);
        }
        public async Task<SellOrder> GetSellOrderByIdAsync(int sellOrderId)
        {
            return await _context.SellOrders
            .Include(so => so.SellOrderConsumesSupplyOrders)
            .Where(so => so.Id == sellOrderId)
            .FirstOrDefaultAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PagedList<SellOrder>> GetSellOrdersAsync(OrderParams sellOrderParams)
        {
            var query = _context.SellOrders.AsQueryable();

            if (!string.IsNullOrEmpty(sellOrderParams.SearchTerm)) query = query
                .Where(u => u.ItemName.ToLower().Contains(sellOrderParams.SearchTerm.ToLower()) || 
                    u.Note.ToLower().Contains(sellOrderParams.SearchTerm.ToLower()));
            if (sellOrderParams.InventoryItemId != null)
                query = query.Where(u => u.InventoryItemId == sellOrderParams.InventoryItemId);

            query = (sellOrderParams.OrderBy, sellOrderParams.Order) switch
            {
                ("dateUpdated", "asc") => query.OrderBy(u => u.DateUpdated),
                ("dateUpdated", "desc") => query.OrderByDescending(u => u.DateUpdated),
                ("dateCreated", "asc") => query.OrderBy(u => u.DateCreated),
                ("dateCreated", "desc") => query.OrderByDescending(u => u.DateCreated),
                _ => query.OrderByDescending(u => u.DateCreated),
            };
            return await PagedList<SellOrder>.CreateAsync(query, sellOrderParams.PageNumber, sellOrderParams.PageSize);
        }
    }
}