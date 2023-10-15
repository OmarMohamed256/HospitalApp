using API.Helpers;
using API.Helpers.Params;
using API.Models.DTOS;
using API.Models.Entities;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services.Implementations
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _medicineRepository;
        private readonly IMapper _mapper;
        public MedicineService(IMedicineRepository medicineRepository, IMapper mapper)
        {
            _medicineRepository = medicineRepository;
            _mapper = mapper;
        }
        public async Task<MedicineDto> CreateUpdateMedicine(MedicineDto medicineDto)
        {
            var newMedicine = _mapper.Map<Medicine>(medicineDto);
            var oldMedicine = await _medicineRepository.GetMedicineByIdAsync(newMedicine.Id);

            if (oldMedicine == null) _medicineRepository.AddMedicine(newMedicine);
            else _medicineRepository.UpdateMedicine(newMedicine);
            
            if (await _medicineRepository.SaveAllAsync()) return _mapper.Map<MedicineDto>(newMedicine);
            throw new Exception("Failed to add/update medicine");
        }

        public async Task DeleteMedicine(int medicineId)
        {
            var medicine = await _medicineRepository.GetMedicineByIdAsync(medicineId) ?? throw new Exception("Medicine does not exist");
            _medicineRepository.DeleteMedicine(medicine);
            if (!await _medicineRepository.SaveAllAsync()) throw new Exception("Failed to delete medicine");
        }

        public async Task<PagedList<MedicineDto>> GetAllMedicinesAsync(MedicineParams medicineParams)
        {
            PagedList<Medicine> medicines = await _medicineRepository.GetAllMedicinesAsync(medicineParams);
            var medicinesDto = _mapper.Map<IEnumerable<MedicineDto>>(medicines);
            return new PagedList<MedicineDto>(medicinesDto, medicines.TotalCount, medicines.CurrentPage, medicines.PageSize);
        }
    }
}