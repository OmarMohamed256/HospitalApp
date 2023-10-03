using API.Constants;
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
        public async Task<IEnumerable<DoctorServiceDto>> GetDoctorServiceWithServiceByDoctorId(int doctorId)
        {
            var services = await _doctorServiceRepository.GetDoctorServiceWithServiceByDoctorId(doctorId);
            return _mapper.Map<IEnumerable<DoctorServiceDto>>(services);
        }

        public async Task<DoctorServiceDto> GetDoctorServiceById(int Id)
        {
            var doctorService = await _doctorServiceRepository.GetDoctorServiceById(Id);
            return _mapper.Map<DoctorServiceDto>(doctorService);
        }

        public async Task<DoctorServiceUpdateDto> UpdateDoctorService(DoctorServiceUpdateDto doctorServiceUpdateDto)
        {
            var doctorService = await _doctorServiceRepository.GetDoctorServiceById(doctorServiceUpdateDto.Id) ?? throw new ApiException(HttpStatusCode.NotFound, "Service does not exist");
            doctorService.HospitalPercentage = doctorServiceUpdateDto.HospitalPercentage;
            doctorService.DoctorPercentage = doctorServiceUpdateDto.DoctorPercentage;

            _doctorServiceRepository.UpdateDoctorService(doctorService);
            bool updateDoctorService = await _doctorServiceRepository.SaveAllAsync();
            if (updateDoctorService) return doctorServiceUpdateDto;

            throw new ApiException(HttpStatusCode.InternalServerError, "Failed to update DoctorService");
        }
    }
}