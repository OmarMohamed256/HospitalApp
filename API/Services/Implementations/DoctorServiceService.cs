using API.Errors;
using API.Models.DTOS;
using API.Models.Entities;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services.Implementations
{
    public class DoctorServiceService : IDoctorServiceService
    {
        private readonly IMapper _mapper;
        private readonly IDoctorServiceRepository _doctorServiceRepository;
        public DoctorServiceService(IMapper mapper, IDoctorServiceRepository doctorServiceRepository)
        {
            _mapper = mapper;
            _doctorServiceRepository = doctorServiceRepository;
        }
        public async Task<IEnumerable<DoctorServiceDto>> GetDoctorServiceByDoctorId(int doctorId)
        {
            var services = await _doctorServiceRepository.GetDoctorServiceByDoctorId(doctorId);
            return _mapper.Map<IEnumerable<DoctorServiceDto>>(services);
        }

        public async Task<DoctorServiceDto> GetDoctorServiceById(int Id)
        {
            var doctorService = await _doctorServiceRepository.GetDoctorServiceById(Id);
            return _mapper.Map<DoctorServiceDto>(doctorService);
        }

        public async Task<DoctorServiceUpdateDto> UpdateDoctorService(DoctorServiceUpdateDto doctorServiceUpdateDto)
        {
            var doctorService = await _doctorServiceRepository.GetDoctorServiceById(doctorServiceUpdateDto.Id) ?? throw new ApiException(404, "Service does not exist");
            doctorService.HospitalPercentage = doctorServiceUpdateDto.HospitalPercentage;
            doctorService.DoctorPercentage = doctorServiceUpdateDto.DoctorPercentage;

            _doctorServiceRepository.UpdateDoctorService(doctorService);

            if (await _doctorServiceRepository.SaveAllAsync()) return doctorServiceUpdateDto;

            throw new ApiException(500, "Failed to update DoctorService");
        }
    }
}