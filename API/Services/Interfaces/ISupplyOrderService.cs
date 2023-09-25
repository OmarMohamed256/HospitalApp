using API.Helpers;
using API.Models.DTOS;

namespace API.Services.Interfaces
{
    public interface ISupplyOrderService
    {
        Task<SupplyOrderDto> CreateUpdateSupplyOrderAsync(SupplyOrderDto supplyOrderDto);
        Task<PagedList<SupplyOrderDto>> GetSupplyOrdersAsync(SupplyOrderParams supplyOrderParams);
    }
}