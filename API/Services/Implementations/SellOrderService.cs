using API.Errors;
using API.Helpers;
using API.Models.DTOS.InventoryDtos;
using API.Models.Entities;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services.Implementations
{
    public class SellOrderService : ISellOrderService
    {
        private readonly ISellOrderRepository _sellOrderRepository;
        private readonly ISupplyOrderRepository _supplyOrderRepository;
        private readonly IMapper _mapper;
        public SellOrderService(ISellOrderRepository sellOrderRepository, IMapper mapper, ISupplyOrderRepository supplyOrderRepository)
        {
            _sellOrderRepository = sellOrderRepository;
            _mapper = mapper;
            _supplyOrderRepository = supplyOrderRepository;
        }
        public async Task<SellOrderDto> CreateUpdateSellOrderAsync(SellOrderDto sellOrderDto)
        {
            // check if enough quantity item exists in supply order
            if (sellOrderDto.InventoryItemId.HasValue && string.IsNullOrEmpty(sellOrderDto.ItemName))
            {
                var consumableSupplyOrders = await _supplyOrderRepository.GetConsumableSupplyOrdersByInventoryItemId(sellOrderDto.InventoryItemId.Value) ??
                    throw new BadRequestException("Not enough supply orders found");
                int quantityNeeded = sellOrderDto.Quantity;
                int totalQuantity = consumableSupplyOrders.Sum(order => order.Quantity);
                if (totalQuantity < quantityNeeded)
                    throw new BadRequestException("Not enough supply orders to fulfill quantity needed for item ");
                foreach (var supplyOrder in consumableSupplyOrders)
                {
                    if (quantityNeeded > 0)
                    {
                        int quantityToConsume = Math.Min(quantityNeeded, supplyOrder.Quantity);
                        supplyOrder.Quantity -= quantityToConsume;
                        supplyOrder.ConsumedQuantity += quantityToConsume;
                        quantityNeeded -= quantityToConsume;
                        _supplyOrderRepository.UpdateSupplyOrder(supplyOrder);
                    }
                    else break;
                    
                }
            }
            // if so create else throw error
            throw new BadRequestException("No Item Was Selected");
        }

        public async Task<PagedList<SellOrderDto>> GetSellOrdersAsync(OrderParams sellOrderParams)
        {
            PagedList<SellOrder> sellOrders = await _sellOrderRepository.GetSellOrdersAsync(sellOrderParams);

            var sellOrdersDto = _mapper.Map<IEnumerable<SellOrderDto>>(sellOrders);

            return new PagedList<SellOrderDto>(sellOrdersDto, sellOrders.TotalCount, sellOrders.CurrentPage, sellOrders.PageSize);
        }
    }
}