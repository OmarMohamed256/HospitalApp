using API.Helpers;
using API.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface IInventoryItemRepository
    {
        void AddInventoryItem(InventoryItem inventoryItem);
        void UpdateInventoryItem(InventoryItem inventoryItem);
        Task<PagedList<InventoryItem>> GetInventoryItemsAsync(InventoryItemParams inventoryItemParams);
        Task<InventoryItem> GetInventoryItemAsync(int inventoryItemId);
        Task<bool> SaveAllAsync();
    }
}