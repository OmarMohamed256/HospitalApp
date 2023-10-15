using API.Extenstions;
using API.Helpers.Params;
using API.Models.DTOS;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MedicineController : BaseApiController
    {
        private readonly IMedicineService _medicineService;
        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<MedicineDto>>> GetMedicinesAsync([FromQuery] MedicineParams medicineParams)
        {
            var medicines = await _medicineService.GetAllMedicinesAsync(medicineParams);
            Response.AddPaginationHeader(medicines.CurrentPage, medicines.PageSize, medicines.TotalCount, medicines.TotalPages);
            return Ok(medicines);
        }
        [HttpPost]
        public async Task<ActionResult<MedicineDto>> CreateMedicineAsync(MedicineDto medicine)
        {
            var newMedicine = await _medicineService.CreateUpdateMedicine(medicine);
            return Ok(newMedicine);
        }
        [HttpPut]
        public async Task<ActionResult<MedicineDto>> UpdateMedicineAsync(MedicineDto medicine)
        {
            var newMedicine = await _medicineService.CreateUpdateMedicine(medicine);
            return Ok(newMedicine);
        }
        [HttpDelete("{medicineId}")]
        public async Task<ActionResult> DeleteRoomAsync(int medicineId)
        {
            await _medicineService.DeleteMedicine(medicineId);
            return NoContent();
        }
    }
}