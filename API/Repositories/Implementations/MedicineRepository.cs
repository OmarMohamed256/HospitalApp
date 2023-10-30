using API.Helpers;
using API.Helpers.Params;
using API.Models.Entities;
using API.Repositories.Interfaces;
using Hospital.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly ApplicationDbContext _context;
        public MedicineRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddMedicine(Medicine medicine)
        {
            _context.Medicines.Add(medicine);
        }

        public void UpdateMedicine(Medicine medicine)
        {
            _context.Medicines.Update(medicine);
        }

        public void DeleteMedicine(Medicine medicine)
        {
            _context.Medicines.Remove(medicine);
        }

        public async Task<PagedList<Medicine>> GetAllMedicinesAsync(MedicineParams medicineParams)
        {
            var query = _context.Medicines.AsQueryable();
            if (!string.IsNullOrEmpty(medicineParams.SearchTerm))
                query = query.Where(m => m.Name.ToLower().Contains(medicineParams.SearchTerm.ToLower()));
            return await PagedList<Medicine>.CreateAsync(query, medicineParams.PageNumber, medicineParams.PageSize);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Medicine> GetMedicineByIdAsync(int medicineId)
        {
            return await _context.Medicines
                .AsNoTracking()
                .Where(m => m.Id == medicineId)
                .FirstOrDefaultAsync();
        }
    }
}