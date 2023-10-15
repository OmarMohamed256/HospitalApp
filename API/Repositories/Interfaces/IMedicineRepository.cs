using API.Helpers;
using API.Helpers.Params;
using API.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface IMedicineRepository
    {
        void AddMedicine(Medicine medicine);
        void UpdateMedicine(Medicine medicine);
        void DeleteMedicine(Medicine medicine);
        Task <PagedList<Medicine>> GetAllMedicinesAsync(MedicineParams medicineParams);
        Task<bool> SaveAllAsync();
        Task <Medicine> GetMedicineByIdAsync(int medicineId);
    }
}