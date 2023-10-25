using API.Models.DTOS.InventoryDtos;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class SellOrderController : BaseApiController
    {
        private readonly ISellOrderService _sellOrderService;
        public SellOrderController(ISellOrderService sellOrderService)
        {
            _sellOrderService = sellOrderService;
        }
        [HttpPost]
        public async Task<ActionResult<SellOrderDto>> CreateSellOrderAsync(SellOrderDto sellOrderDto)
        {
            return await _sellOrderService.CreateUpdateSellOrderAsync(sellOrderDto);
        }
        [HttpPut]
        public async Task<ActionResult<SellOrderDto>> UpdateSellOrderAsync(SellOrderDto sellOrderDto)
        {
            return await _sellOrderService.CreateUpdateSellOrderAsync(sellOrderDto);
        }
    }
}