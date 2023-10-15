using API.Helpers;
using API.Helpers.Params;
using API.Models.DTOS;

namespace API.Services.Interfaces
{
    public interface IMedicineService
    {
        Task<MedicineDto> CreateUpdateMedicine(MedicineDto medicineDto);
        Task<PagedList<MedicineDto>> GetAllMedicinesAsync(MedicineParams medicineParams);
        Task DeleteMedicine(int medicineId);
    }
}