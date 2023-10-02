using API.Helpers;
using API.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface ISupplyOrderRepository
    {
        void AddSupplyOrder(SupplyOrder supplyOrder);
        void UpdateSupplyOrder(SupplyOrder supplyOrder);
        Task<PagedList<SupplyOrder>> GetSupplyOrdersAsync(SupplyOrderParams supplyOrderParams);
        Task<ICollection<SupplyOrder>> GetConsumableSupplyOrdersByInventoryItemId(int inventoryItemId);
        void UpdateSupplyOrdersRange(List<SupplyOrder> supplyOrders);
        Task<bool> SaveAllAsync();
    }
}