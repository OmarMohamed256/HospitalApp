using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class InventoryItemService : IInventoryItemService
    {
        private readonly IInventoryItemRepository _inventoryItemRepository;
        private readonly IMapper _mapper;
        public InventoryItemService(IInventoryItemRepository inventoryItemRepository, IMapper mapper)
        {
            _inventoryItemRepository = inventoryItemRepository;
            _mapper = mapper;
        }
        public async Task<InventoryItemDto> CreateUpdateInventoryItemAsync(InventoryItemDto inventoryItemDto)
        {
            var inventoryItem = _mapper.Map<InventoryItem>(inventoryItemDto);

            if (inventoryItem.Id != 0) _inventoryItemRepository.UpdateInventoryItem(inventoryItem);
            else _inventoryItemRepository.AddInventoryItem(inventoryItem);
            
            var result = await _inventoryItemRepository.SaveAllAsync();
            if (result) return _mapper.Map<InventoryItemDto>(inventoryItem);

            throw new ApiException(HttpStatusCode.InternalServerError, "Failed to add/update inventory item");
        }

        public async Task<PagedList<InventoryItemDto>> GetInventoryItemsAsync(InventoryItemParams inventoryItemParams)
        {
            PagedList<InventoryItem> inventoryItems = await _inventoryItemRepository.GetInventoryItemsAsync(inventoryItemParams);

            var inventoryItemsDto = _mapper.Map<IEnumerable<InventoryItemDto>>(inventoryItems);

            return new PagedList<InventoryItemDto>(inventoryItemsDto, inventoryItems.TotalCount, inventoryItems.CurrentPage, inventoryItems.PageSize);
        }


    }
}