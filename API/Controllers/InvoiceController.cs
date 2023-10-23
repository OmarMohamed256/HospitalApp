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
        [HttpPut]
        public async Task<ActionResult<InvoiceDto>> UpdateInvoiceAsync(CreateInvoiceDto invoiceDto)
        {
            return await _invoiceService.UpdateInvoiceAsync(invoiceDto);
        }
        [HttpGet("{invoiceId}")]
        public async Task<ActionResult<InvoiceDto>> CreateInvoiceAsync(int invoiceId)
        {
            return await _invoiceService.GetInvoiceByIdAsync(invoiceId);
        }
        [HttpGet]
        [Route("getMedicinesByInvoiceId/{invoiceId}")]
        public async Task<ActionResult<ICollection<MedicineDto>>> GetMedicinesByInvoiceId (int invoiceId)
        {
            var medicines = await _invoiceService.GetMedicinesByInvoiceId(invoiceId);
            return Ok(medicines);
        }
    }
}