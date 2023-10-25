using API.Helpers;
using API.Models.DTOS.InventoryDtos;

namespace API.Services.Interfaces
{
    public interface ISellOrderService
    {
        Task<PagedList<SellOrderDto>> GetSellOrdersAsync(OrderParams sellOrderParams);
        Task<SellOrderDto> CreateUpdateSellOrderAsync(SellOrderDto sellOrderDto);
    }
}