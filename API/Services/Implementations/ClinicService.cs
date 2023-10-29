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

        public async Task<ClinicDto> CreateUpdateClinic(CreateUpdateClinicDto clinic)
        {
            var existingClinic = await _clinicRepository.GetClinicById(clinic.Id);
            if (existingClinic == null) _clinicRepository.AddClinic(_mapper.Map<Clinic>(clinic));
            else
            {
                existingClinic.ClinicDoctors = _mapper.Map<ICollection<ClinicDoctor>>(clinic.ClinicDoctors);
                existingClinic.ClinicNumber = clinic.ClinicNumber;
                _clinicRepository.UpdateClinic(_mapper.Map<Clinic>(existingClinic));
            }
            if (await _clinicRepository.SaveAllAsync()) return _mapper.Map<ClinicDto>(existingClinic);
            throw new Exception("Failed to add/update clinic");
        }

        public async Task DeleteClinic(int clinicId)
        {
            var clinic = await _clinicRepository.GetClinicById(clinicId) ?? throw new Exception("Clinic not found");
            _clinicRepository.DeleteClinic(clinic);
            if (!await _clinicRepository.SaveAllAsync()) throw new Exception("Can not delete clinic");
        }

        public async Task<ICollection<ClinicDto>> GetClinicsWithFirstTwoUpcomingAppointmentsAsync()
        {
            ICollection<Clinic> clinics = await _clinicRepository.GetClinicsWithFirstTwoUpcomingAppointmentsAsync();
            return _mapper.Map<ICollection<ClinicDto>>(clinics);
        }

        public async Task<ClinicDto> GetClinicByIdAsync(int clinicId)
        {
            var clinic = await _clinicRepository.GetClinicById(clinicId) ?? throw new Exception("Clinic not found");
            return _mapper.Map<ClinicDto>(clinic);
        }
    }
}