using System.ComponentModel.DataAnnotations;
using API.Extenstions;
using API.Models.DTOS;
using API.Services.Interfaces;
using HospitalApp.Constants;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPut]
        [Authorize(Policy = Polices.RequireDoctorRole)]
        public async Task<ActionResult<InvoiceDto>> UpdateInvoiceAsync(CreateInvoiceDto invoiceDto)
        {
            var userRole = User.GetUserRole();
            return await _invoiceService.UpdateInvoiceByUserRoleAsync(invoiceDto, userRole);
        }
        [HttpGet("{invoiceId}")]
        public async Task<ActionResult<InvoiceDto>> CreateInvoiceAsync(int invoiceId)
        {
            return await _invoiceService.GetInvoiceByIdAsync(invoiceId);
        }
        [HttpGet]
        [Route("getMedicinesByInvoiceId/{invoiceId}")]
        [Authorize(Policy = Polices.RequireDoctorRole)]
        public async Task<ActionResult<ICollection<MedicineDto>>> GetMedicinesByInvoiceId (int invoiceId)
        {
            var medicines = await _invoiceService.GetMedicinesByInvoiceId(invoiceId);
            return Ok(medicines);
        }
        [HttpPut]
        [Route("updateInvoiceDebt/{invoiceId}")]
        [Authorize(Policy = Polices.RequireReceptionistRole)]
        public async Task<ActionResult<decimal>> UpdateInvoiceDebt(int invoiceId, [FromBody] [Required]int totalPaid)
        {
            var totalRemaning = await _invoiceService.UpdateInvoiceDebt(invoiceId, totalPaid);
            return Ok(totalRemaning);
        }
    }
}