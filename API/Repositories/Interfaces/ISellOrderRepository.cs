using API.Helpers;
using API.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface ISellOrderRepository
    {
        void AddSellOrder(SellOrder sellOrder);
        void UpdateSellOrder(SellOrder sellOrder);
        Task<PagedList<SellOrder>> GetSellOrdersAsync(OrderParams sellOrderParams);
        Task<SellOrder> GetSellOrderByIdAsync(int sellOrderId);
        Task<bool> SaveAllAsync();        
    }
}