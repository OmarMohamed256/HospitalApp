using API.Constants;
using API.Errors;
using API.Helpers;
using API.Models.DTOS;
using API.Models.Entities;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services.Implementations
{
    public class SupplyOrderService : ISupplyOrderService
    {
        private readonly ISupplyOrderRepository _supplyOrderRepository;
        private readonly IMapper _mapper;
        private readonly IInventoryItemRepository _inventoryItemRepository;
        public SupplyOrderService(ISupplyOrderRepository supplyOrderRepository, IMapper mapper,
            IInventoryItemRepository inventoryItemRepository)
        {
            _supplyOrderRepository = supplyOrderRepository;
            _mapper = mapper;
            _inventoryItemRepository = inventoryItemRepository;
        }
        public async Task<SupplyOrderDto> CreateUpdateSupplyOrderAsync(SupplyOrderDto supplyOrderDto)
        {
            if (supplyOrderDto.InventoryItemId.HasValue && string.IsNullOrEmpty(supplyOrderDto.ItemName))
            {
                var item = await _inventoryItemRepository.GetInventoryItemAsync(supplyOrderDto.InventoryItemId.Value);
                supplyOrderDto.ItemName = item.Name;
            }
            var supplyOrder = _mapper.Map<SupplyOrder>(supplyOrderDto);

            if (supplyOrder.Id != 0) _supplyOrderRepository.AddSupplyOrder(supplyOrder);
            else _supplyOrderRepository.UpdateSupplyOrder(supplyOrder);

            var result = await _supplyOrderRepository.SaveAllAsync();
            if (result) return _mapper.Map<SupplyOrderDto>(supplyOrder);

            throw new ApiException(HttpStatusCode.InternalServerError, "Failed to add/update supply order");
        }

        public async Task<PagedList<SupplyOrderDto>> GetSupplyOrdersAsync(SupplyOrderParams supplyOrderParams)
        {
            PagedList<SupplyOrder> supplyOrders = await _supplyOrderRepository.GetSupplyOrdersAsync(supplyOrderParams);

            var supplyOrdersDto = _mapper.Map<IEnumerable<SupplyOrderDto>>(supplyOrders);

            return new PagedList<SupplyOrderDto>(supplyOrdersDto, supplyOrders.TotalCount, supplyOrders.CurrentPage, supplyOrders.PageSize);
        }
    }
}