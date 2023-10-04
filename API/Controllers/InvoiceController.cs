using API.Models.DTOS;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class InvoiceController : BaseApiController
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        [HttpPost]
        public async Task<ActionResult<InvoiceDto>> CreateInvoiceAsync(CreateInvoiceDto invoiceDto)
        {
            return await _invoiceService.CreateInvoiceAsync(invoiceDto);
        }
        [HttpGet("{invoiceId}")]
        public async Task<ActionResult<InvoiceDto>> CreateInvoiceAsync(int invoiceId)
        {
            return await _invoiceService.GetInvoiceByIdAsync(invoiceId);
        }
    }
}