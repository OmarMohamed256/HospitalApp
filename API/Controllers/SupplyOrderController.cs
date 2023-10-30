using API.Extenstions;
using API.Helpers;
using API.Models.DTOS;
using API.Services.Interfaces;
using HospitalApp.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Policy = Polices.RequireAdminRole)]
    public class SupplyOrderController : BaseApiController
    {
        private readonly ISupplyOrderService _supplyOrderService;

        public SupplyOrderController(ISupplyOrderService supplyOrderService)
        {
            _supplyOrderService = supplyOrderService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<SupplyOrderDto>>> GetSupplyOrdersAsync(
            [FromQuery] OrderParams supplyOrderParams)
        {
            var supplyOrders = await _supplyOrderService.GetSupplyOrdersAsync(supplyOrderParams);
            Response.AddPaginationHeader(supplyOrders.CurrentPage, supplyOrders.PageSize, supplyOrders.TotalCount, supplyOrders.TotalPages);
            return Ok(supplyOrders);
        }

        [HttpPost]
        public async Task<ActionResult<SupplyOrderDto>> CreateSupplyOrderAsync(SupplyOrderDto supplyOrderDto)
        {
            return await _supplyOrderService.CreateUpdateSupplyOrderAsync(supplyOrderDto);
        }
        [HttpPut]
        public async Task<ActionResult<SupplyOrderDto>> UpdateSupplyOrderAsync(SupplyOrderDto supplyOrderDto)
        {
            return await _supplyOrderService.CreateUpdateSupplyOrderAsync(supplyOrderDto);
        }
    }
}