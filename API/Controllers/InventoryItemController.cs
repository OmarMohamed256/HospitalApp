using API.Extenstions;
using API.Helpers;
using API.Models.DTOS;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class InventoryItemController : BaseApiController
    {
        private readonly IInventoryItemService _inventoryItemService;

        public InventoryItemController(IInventoryItemService inventoryItemService)
        {
            _inventoryItemService = inventoryItemService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<InventoryItemDto>>> GetInventoryItemsAsync(
            [FromQuery] InventoryItemParams inventoryItemParams)
        {
            var inventoryItems = await _inventoryItemService.GetInventoryItemsAsync(inventoryItemParams);
            Response.AddPaginationHeader(inventoryItems.CurrentPage, inventoryItems.PageSize, inventoryItems.TotalCount, inventoryItems.TotalPages);
            return Ok(inventoryItems);
        }

        [HttpPost]
        public async Task<ActionResult<InventoryItemDto>> CreateInventoryItemAsync(InventoryItemDto inventoryItemDto)
        {
            return await _inventoryItemService.CreateUpdateInventoryItemAsync(inventoryItemDto);
        }
        [HttpPut]
        public async Task<ActionResult<InventoryItemDto>> UpdateInventoryItemAsync(InventoryItemDto inventoryItemDto)
        {
            return await _inventoryItemService.CreateUpdateInventoryItemAsync(inventoryItemDto);
        }
    }
}