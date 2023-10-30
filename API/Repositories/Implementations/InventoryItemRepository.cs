using API.Helpers;
using API.Models.Entities;
using API.Repositories.Interfaces;
using Hospital.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations
{
    public class InventoryItemRepository : IInventoryItemRepository
    {
        private readonly ApplicationDbContext _context;
        public InventoryItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddInventoryItem(InventoryItem inventoryItem)
        {
            _context.Add(inventoryItem);
        }

        public async Task<InventoryItem> GetInventoryItemAsync(int inventoryItemId)
        {
            return await _context.InventoryItems
                .AsNoTracking()
                .FirstOrDefaultAsync(ii => ii.Id == inventoryItemId);
        }

        public async Task<PagedList<InventoryItem>> GetInventoryItemsAsync(InventoryItemParams inventoryItemParams)
        {
            var query = _context.InventoryItems.AsQueryable();

            if (inventoryItemParams.SpecialityId != null)
                query = query.Where(u => u.InventoryItemSpecialityId == inventoryItemParams.SpecialityId);

            if (!string.IsNullOrEmpty(inventoryItemParams.SearchTerm)) 
                query = query.Where(u => u.Name.ToLower().Contains(inventoryItemParams.SearchTerm.ToLower()));
            return await PagedList<InventoryItem>.CreateAsync(query, inventoryItemParams.PageNumber, inventoryItemParams.PageSize);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateInventoryItem(InventoryItem inventoryItem)
        {
            _context.Update(inventoryItem);
        }
    }
}