using API.Helpers;
using API.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface ISupplyOrderRepository
    {
        void AddSupplyOrder(SupplyOrder supplyOrder);
        void UpdateSupplyOrder(SupplyOrder supplyOrder);
        Task<PagedList<SupplyOrder>> GetSupplyOrdersAsync(OrderParams supplyOrderParams);
        Task<SupplyOrder> GetSupplyOrderByIdAsync(int supplyOrderId);
        Task<ICollection<SupplyOrder>> GetConsumableSupplyOrdersByInventoryItemId(int inventoryItemId, bool includeExpired = false);
        void UpdateSupplyOrdersRange(List<SupplyOrder> supplyOrders);
        Task<bool> SaveAllAsync();
    }
}