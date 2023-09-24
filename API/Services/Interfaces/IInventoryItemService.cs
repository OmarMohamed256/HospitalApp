using API.Helpers;
using API.Models.DTOS;

namespace API.Services.Interfaces
{
    public interface IInventoryItemService
    {
        Task<InventoryItemDto> CreateUpdateInventoryItemAsync(InventoryItemDto inventoryItemDto);
        Task<PagedList<InventoryItemDto>> GetInventoryItemsAsync(InventoryItemParams inventoryItemParams);
    }
}