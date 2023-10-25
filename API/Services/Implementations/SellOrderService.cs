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
        private readonly IInventoryItemRepository _inventoryItemRepository;
        private readonly IMapper _mapper;
        public SellOrderService(ISellOrderRepository sellOrderRepository, IMapper mapper,
         ISupplyOrderRepository supplyOrderRepository, IInventoryItemRepository inventoryItemRepository)
        {
            _sellOrderRepository = sellOrderRepository;
            _mapper = mapper;
            _supplyOrderRepository = supplyOrderRepository;
            _inventoryItemRepository = inventoryItemRepository;
        }
        public async Task<SellOrderDto> CreateUpdateSellOrderAsync(SellOrderDto sellOrderDto)
        {
            var sellOrder = await _sellOrderRepository.GetSellOrderByIdAsync(sellOrderDto.Id);
            if (sellOrder == null)
            {
                // add sell order
                // check if enough quantity item exists in supply order
                var consumableSupplyOrders =
                    await _supplyOrderRepository.GetConsumableSupplyOrdersByInventoryItemId(sellOrderDto.InventoryItemId,
                     sellOrderDto.IncludeExpiredItems) ??
                    throw new BadRequestException("Not enough supply orders found");
                sellOrder = new()
                {
                    SellPrice = sellOrderDto.SellPrice,
                    Quantity = sellOrderDto.Quantity,
                    InventoryItemId = sellOrderDto.InventoryItemId,
                    IncludeExpiredItems = sellOrderDto.IncludeExpiredItems,
                    SellOrderConsumesSupplyOrders =
                        ConsumeSupplyOrdersAndReturnConsumedSellSupply(consumableSupplyOrders, sellOrderDto),
                    SoldTo = sellOrderDto.SoldTo,
                    Note = sellOrderDto.Note,
                    ItemName = await GetInventoryItemNameById(sellOrderDto.InventoryItemId)
                };
                _sellOrderRepository.AddSellOrder(sellOrder);
            }
            else
            {
                // can only update SellPrice and note and sold too
                sellOrder.SellPrice = sellOrderDto.SellPrice;
                sellOrder.Note = sellOrderDto.Note;
                sellOrder.SoldTo = sellOrderDto.SoldTo;
                _sellOrderRepository.UpdateSellOrder(sellOrder);
            }
            if (await _sellOrderRepository.SaveAllAsync()) return _mapper.Map<SellOrderDto>(sellOrder);
            throw new Exception("Failed to add sell order");
        }
        private ICollection<SellOrderConsumesSupplyOrder> ConsumeSupplyOrdersAndReturnConsumedSellSupply(ICollection<SupplyOrder> consumableSupplyOrders, SellOrderDto sellOrderDto)
        {
            ICollection<SellOrderConsumesSupplyOrder> sellOrderConsumesSupplyOrders = new List<SellOrderConsumesSupplyOrder>();
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
                    var sellOrderConsumesSupplyOrder = new SellOrderConsumesSupplyOrder
                    {
                        ItemPrice = sellOrderDto.SellPrice,
                        QuantityUsed = quantityToConsume,
                        SupplyOrderId = supplyOrder.Id,
                        TotalPrice = supplyOrder.SellPrice * quantityToConsume
                    };
                    sellOrderConsumesSupplyOrders.Add(sellOrderConsumesSupplyOrder);
                    _supplyOrderRepository.UpdateSupplyOrder(supplyOrder);
                }
                else break;
            }
            return sellOrderConsumesSupplyOrders;
        }

        private async Task<string> GetInventoryItemNameById(int itemId)
        {
            var item = await _inventoryItemRepository.GetInventoryItemAsync(itemId);
            return item.Name;
        }

        public async Task<PagedList<SellOrderDto>> GetSellOrdersAsync(OrderParams sellOrderParams)
        {
            PagedList<SellOrder> sellOrders = await _sellOrderRepository.GetSellOrdersAsync(sellOrderParams);

            var sellOrdersDto = _mapper.Map<IEnumerable<SellOrderDto>>(sellOrders);

            return new PagedList<SellOrderDto>(sellOrdersDto, sellOrders.TotalCount, sellOrders.CurrentPage, sellOrders.PageSize);
        }
    }
}