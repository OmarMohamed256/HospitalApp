using API.Helpers;
using API.Models.DTOS;
using API.Models.Entities;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services.Implementations
{
    public class ClinicService : IClinicService
    {
        private readonly IClinicRepository _clinicRepository;
        private readonly IMapper _mapper;
        public ClinicService(IClinicRepository clinicRepository, IMapper mapper)
        {
            _clinicRepository = clinicRepository;
            _mapper = mapper;
        }

        public async Task<ClinicDto> CreateUpdateClinic(CreateClinicDto clinic)
        {
            var newClinic = _mapper.Map<Clinic>(clinic);
            // catch if there is no duplicate records with same doctor id
            if (newClinic.DoctorId.HasValue)
            {
                var existingClinic = await _clinicRepository.GetClinicByDoctorId(newClinic.DoctorId.Value);
                if (existingClinic != null && existingClinic.Id != newClinic.Id)
                {
                    throw new Exception("A clinic with the same doctor already exists!");
                }
            }
            var oldRoom = await _clinicRepository.GetClinicById(clinic.Id);

            if (oldRoom == null)
                _clinicRepository.AddClinic(newClinic);
            else
                _clinicRepository.UpdateClinic(newClinic);

            if (await _clinicRepository.SaveAllAsync()) return _mapper.Map<ClinicDto>(newClinic);
            throw new Exception("Failed to add/update clinic");
        }

        public async Task DeleteClinic(int clinicId)
        {
            var clinic = await _clinicRepository.GetClinicById(clinicId) ?? throw new Exception("Clinic not found");
            _clinicRepository.DeleteClinic(clinic);
            if(!await _clinicRepository.SaveAllAsync()) throw new Exception("Can not delete clinic");
        }

        public async Task<PagedList<ClinicDto>> GetAllClinicsAsync(ClinicParams clinicParams)
        {
            PagedList<Clinic> clinics = await _clinicRepository.GetAllClinicsWithUpComingAppointmentsAsync(clinicParams);

            var clinicsDto = _mapper.Map<IEnumerable<ClinicDto>>(clinics);

            return new PagedList<ClinicDto>(clinicsDto, clinics.TotalCount, clinics.CurrentPage, clinics.PageSize);
        }

        public async Task<ClinicDto> GetClinicByIdAsync(int clinicId)
        {
            var clinic = await _clinicRepository.GetClinicById(clinicId) ?? throw new Exception("Clinic not found");
            return _mapper.Map<ClinicDto>(clinic);
        }
    }
}